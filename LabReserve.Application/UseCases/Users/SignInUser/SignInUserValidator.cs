using FluentValidation;

namespace LabReserve.Application.UseCases.Users.UserSignIn
{
    public class SignInUserValidator : AbstractValidator<SignInUserCommand>
    {
        public SignInUserValidator()
        {
            RuleFor(request => request.Email).NotNull().EmailAddress();
            RuleFor(request => request.Password).NotNull().MinimumLength(3);
        }
    }
}
