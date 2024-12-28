using FluentValidation;
using FluentValidation.Results;
using LabReserve.Domain.Abstractions;

namespace LabReserve.Application.UseCases.Groups.CreateGroup;

public class CreateGroupValidator : AbstractValidator<CreateGroupCommand>
{
    public CreateGroupValidator(IGroupRepository groupRepository, ICourseRepository courseRepository)
    {
        RuleFor(request => request.Name).NotNull()
            .CustomAsync(async (name, context, cancellationToken) =>
            {
                if (!string.IsNullOrWhiteSpace(name))
                {
                    var groupName = await groupRepository.GetByName(name);
                    if (groupName is not null)
                        context.AddFailure(new ValidationFailure("name", "already registered"));
                }
            });

        RuleFor(Request => Request.CourseId)
            .CustomAsync(async (courseId, context, cancellationToken) =>
            {
                var course = await courseRepository.Get(courseId);
                if (course is null)
                    context.AddFailure(new ValidationFailure("courseId", "course not found"));
            });
    }
}