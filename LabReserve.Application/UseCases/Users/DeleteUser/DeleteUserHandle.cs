using LabReserve.Domain.Abstractions;
using MediatR;

namespace LabReserve.Application.UseCases.Users.DeleteUser;

public class DeleteUserHandle : IRequestHandler<DeleteUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    public DeleteUserHandle(IUserRepository repository, IUnitOfWork unitOfWork)
    {
        _userRepository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _unitOfWork.BeginTransaction();
            var user = await _userRepository.Get(request.UserId) ?? throw new Exception("User not found");
            await _userRepository.Delete(user);
            _unitOfWork.Commit();
        }
        catch (Exception)
        {
            _unitOfWork.Rollback();
            throw;
        }
    }
}
