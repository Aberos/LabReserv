using FluentValidation;
using LabReserve.Domain.Dto;

namespace LabReserve.Application.Validators;

public class AuthRequestValidator : AbstractValidator<AuthRequestDto>
{
    public AuthRequestValidator()
    {
        RuleFor(request => request.Email).NotNull().EmailAddress();
        RuleFor(request => request.Password).NotNull().MinimumLength(3);
    }
}
