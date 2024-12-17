using LabReserve.Domain.Abstractions;
using LabReserve.Domain.Dto;
using LabReserve.Domain.Helpers;
using MediatR;

namespace LabReserve.Application.UseCases.Users.UserSignIn
{
    public class SignInUserHandler : IRequestHandler<SignInUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;
        public SignInUserHandler(IAuthService authService, IUserRepository userRepository)
        {
            _authService = authService;
            _userRepository = userRepository;
        }

        public async Task Handle(SignInUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmail(request.Email) ?? throw new Exception("User not found");
            if (user.Password != UserHelper.EncryptPassword(request.Password))
                throw new Exception("Invalid email or password");

            var userAuth = new UserAuthDto
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.GetFullName(),
                UserType = user.UserType
            };

            await _authService.SetUser(userAuth);
        }
    }
}
