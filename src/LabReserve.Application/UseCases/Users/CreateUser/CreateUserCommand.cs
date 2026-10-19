using LabReserve.Domain.Enums;
using Mediator;

namespace LabReserve.Application.UseCases.Users.CreateUser
{
    public record CreateUserCommand(string Email, string FirstName, string LastName, string Phone, string Password, UserType UserType, List<long> Groups) : ICommand
    {
    }
}
