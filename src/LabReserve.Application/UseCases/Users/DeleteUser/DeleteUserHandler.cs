using LabReserve.Domain.Abstractions;
using Mediator;

namespace LabReserve.Application.UseCases.Users.DeleteUser;

public class DeleteUserHandler(IUserRepository userRepository, IAuthService authService)
    : ICommandHandler<DeleteUserCommand>
{
    public async ValueTask<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.Get(request.UserId) ?? throw new KeyNotFoundException("User not found");
        user.UpdatedBy = authService.Id!.Value;

        await userRepository.Delete(user);

        return Unit.Value;
    }
}
