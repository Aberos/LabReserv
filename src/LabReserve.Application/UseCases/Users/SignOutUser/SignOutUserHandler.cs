using LabReserve.Domain.Abstractions;
using Mediator;

namespace LabReserve.Application.UseCases.Users.UserSignOut
{
    public class SignOutUserHandler : ICommandHandler<SignOutUserCommand>
    {
        private readonly IAuthService _authService;
        public SignOutUserHandler(IAuthService authService)
        {
            _authService = authService;
        }


        public async ValueTask<Unit> Handle(SignOutUserCommand request, CancellationToken cancellationToken)
        {
            await _authService.RemoveUser();

            return Unit.Value;
        }
    }
}
