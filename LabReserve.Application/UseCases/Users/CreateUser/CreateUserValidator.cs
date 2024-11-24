using FluentValidation;
using FluentValidation.Results;
using LabReserve.Domain.Abstractions;

namespace LabReserve.Application.UseCases.Users.CreateUser
{
    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator(IUserRepository userRepository)
        {
            RuleFor(request => request.Email).NotNull().EmailAddress()
            .CustomAsync(async (email, context, cancellationToken) =>
                {
                    if (!string.IsNullOrWhiteSpace(email))
                    {
                        var emailDB = await userRepository.GetByEmail(email);
                        if (emailDB is not null)
                            context.AddFailure(new ValidationFailure("email", "already registered"));
                    }
                });

            RuleFor(request => request.FirstName).NotNull();
            RuleFor(request => request.LastName).NotNull();
            RuleFor(request => request.Phone).NotNull().MinimumLength(10).MaximumLength(11);
            RuleFor(request => request.Password).NotNull().MinimumLength(6);
            RuleFor(request => request.UserType).NotNull().IsInEnum();
        }
    }
}
