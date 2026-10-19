using FluentValidation;
using FluentValidation.Results;
using LabReserve.Domain.Abstractions;

namespace LabReserve.Application.UseCases.Courses.UpdateCourse;

public class UpdateCourseValidator : AbstractValidator<UpdateCourseCommand>
{
    public UpdateCourseValidator(ICourseRepository courseRepository)
    {
        RuleFor(request => request.Name).NotNull()
            .CustomAsync(async (name, context, cancellationToken) =>
            {
                if (!string.IsNullOrWhiteSpace(name))
                {
                    var courseName = await courseRepository.GetByName(name);
                    if (courseName is not null && courseName.Id != context.InstanceToValidate.CourseId)
                        context.AddFailure(new ValidationFailure("name", "already registered"));
                }
            });
    }
}
