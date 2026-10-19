using LabReserve.Domain.Abstractions;
using Mediator;

namespace LabReserve.Application.Behaviors;

public sealed class TransactionBehavior<TMessage, TResponse>(IUnitOfWork unitOfWork)
    : IPipelineBehavior<TMessage, TResponse>
    where TMessage : IBaseCommand
{
    public async ValueTask<TResponse> Handle(
        TMessage message,
        MessageHandlerDelegate<TMessage, TResponse> next,
        CancellationToken cancellationToken)
    {
        unitOfWork.BeginTransaction();
        try
        {
            var response = await next(message, cancellationToken);
            unitOfWork.Commit();
            return response;
        }
        catch
        {
            unitOfWork.Rollback();
            throw;
        }
    }
}
