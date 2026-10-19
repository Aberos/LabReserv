using LabReserve.Domain.Abstractions;
using MediatR;

namespace LabReserve.Application.UseCases.Users.DeleteUser;

public class DeleteUserHandle : IRequestHandler<DeleteUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthService _authService;
    public DeleteUserHandle(IUserRepository repository, IUnitOfWork unitOfWork, IAuthService authService)
    {
        _userRepository = repository;
        _unitOfWork = unitOfWork;
        _authService = authService;
    }

    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _unitOfWork.BeginTransaction();
            var user = await _userRepository.Get(request.UserId) ?? throw new Exception("User not found");
            user.UpdatedBy = _authService.Id!.Value;
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
