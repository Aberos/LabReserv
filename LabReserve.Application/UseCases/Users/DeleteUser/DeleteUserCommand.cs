using MediatR;

namespace LabReserve.Application.UseCases.Users.DeleteUser;

public record DeleteUserCommand(long UserId) : IRequest
{

}
