using Mediator;

namespace LabReserve.Application.UseCases.Users.DeleteUser;

public record DeleteUserCommand(long UserId) : ICommand
{

}
