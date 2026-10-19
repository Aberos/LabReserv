using FluentValidation;
using LabReserve.Domain.Abstractions;
using MediatR;

namespace LabReserve.Application.UseCases.Groups.UpdateGroup;

public class UpdateGroupHandler(
    IGroupRepository groupRepository,
    IUnitOfWork unitOfWork,
    IAuthService authService,
    IValidator<UpdateGroupCommand> validator) : IRequestHandler<UpdateGroupCommand>
{
    public async Task Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        try
        {
            unitOfWork.BeginTransaction();

            var group = await groupRepository.Get(request.GroupId) ?? throw new KeyNotFoundException("Group not found");
            group.Name = request.Name;
            group.CourseId = request.CourseId;
            group.UpdatedBy = authService.Id!.Value;

            await groupRepository.Update(group);
            unitOfWork.Commit();
        }
        catch
        {
            unitOfWork.Rollback();
            throw;
        }
    }
}
