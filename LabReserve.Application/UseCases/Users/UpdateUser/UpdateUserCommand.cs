using LabReserve.Domain.Enums;
using MediatR;

namespace LabReserve.Application.UseCases.Users.UpdateUser;

public record UpdateUserCommand(string Email, string FirstName, string LastName, string Phone, string Password, UserType UserType, List<long> Groups, long UserId) : IRequest
{

}
