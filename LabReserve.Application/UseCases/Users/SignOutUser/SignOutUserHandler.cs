using LabReserve.Domain.Abstractions;
using MediatR;

namespace LabReserve.Application.UseCases.Users.UserSignOut
{
    public class SignOutUserHandler : IRequestHandler<SignOutUserCommand>
    {
        private readonly IAuthService _authService;
        public SignOutUserHandler(IAuthService authService)
        {
            _authService = authService;
        }


        public async Task Handle(SignOutUserCommand request, CancellationToken cancellationToken)
        {
            await _authService.RemoveUser();
        }
    }
}
