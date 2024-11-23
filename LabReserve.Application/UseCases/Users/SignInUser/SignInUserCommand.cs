using MediatR;

namespace LabReserve.Application.UseCases.Users.UserSignIn
{
    public record SignInUserCommand(string Email, string Password) : IRequest
    {
    }
}
