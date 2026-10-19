using LabReserve.Domain.Abstractions;
using Mediator;

namespace LabReserve.Application.UseCases.Courses.UpdateCourse;

public class UpdateCourseHandler(ICourseRepository courseRepository, IAuthService authService)
    : ICommandHandler<UpdateCourseCommand>
{
    public async ValueTask<Unit> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await courseRepository.Get(request.CourseId) ?? throw new KeyNotFoundException("Course not found");
        course.Name = request.Name;
        course.UpdatedBy = authService.Id!.Value;

        await courseRepository.Update(course);

        return Unit.Value;
    }
}
