using System;
using FluentValidation;
using FluentValidation.Results;
using LabReserve.Domain.Abstractions;

namespace LabReserve.Application.UseCases.Courses.CreateCourse;

public class CreateCourseValidator : AbstractValidator<CreateCourseCommand>
{
    public CreateCourseValidator(ICourseRepository courseRepository)
    {
        RuleFor(request => request.Name).NotNull()
            .CustomAsync(async (name, context, cancellationToken) =>
            {
                if (!string.IsNullOrWhiteSpace(name))
                {
                    var courseName = await courseRepository.GetByName(name);
                    if (courseName is not null)
                        context.AddFailure(new ValidationFailure("name", "already registered"));
                }
            });
    }
}
