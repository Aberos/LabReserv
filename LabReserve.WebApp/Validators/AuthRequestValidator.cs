using FluentValidation;
using LabReserve.WebApp.Domain.Dto;

namespace LabReserve.WebApp.Validators
{
    public class AuthRequestValidator : AbstractValidator<AuthRequestDto>
    {
        public AuthRequestValidator()
        {
            RuleFor(request => request.Email).NotNull().EmailAddress();
            RuleFor(request => request.Password).NotNull().MinimumLength(3);
        }
    }
}
