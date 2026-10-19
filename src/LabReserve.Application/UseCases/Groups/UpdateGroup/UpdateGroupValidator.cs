using FluentValidation;
using FluentValidation.Results;
using LabReserve.Domain.Abstractions;

namespace LabReserve.Application.UseCases.Groups.UpdateGroup;

public class UpdateGroupValidator : AbstractValidator<UpdateGroupCommand>
{
    public UpdateGroupValidator(IGroupRepository groupRepository, ICourseRepository courseRepository)
    {
        RuleFor(request => request.Name).NotNull()
            .CustomAsync(async (name, context, cancellationToken) =>
            {
                if (!string.IsNullOrWhiteSpace(name))
                {
                    var groupName = await groupRepository.GetByName(name);
                    if (groupName is not null && groupName.Id != context.InstanceToValidate.GroupId)
                        context.AddFailure(new ValidationFailure("name", "already registered"));
                }
            });

        RuleFor(request => request.CourseId)
            .CustomAsync(async (courseId, context, cancellationToken) =>
            {
                var course = await courseRepository.Get(courseId);
                if (course is null)
                    context.AddFailure(new ValidationFailure("courseId", "course not found"));
            });
    }
}
