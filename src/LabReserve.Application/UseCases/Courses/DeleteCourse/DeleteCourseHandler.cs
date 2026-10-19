using LabReserve.Domain.Abstractions;
using Mediator;

namespace LabReserve.Application.UseCases.Courses.DeleteCourse;

public class DeleteCourseHandler(ICourseRepository courseRepository, IAuthService authService)
    : ICommandHandler<DeleteCourseCommand>
{
    public async ValueTask<Unit> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await courseRepository.Get(request.CourseId) ?? throw new KeyNotFoundException("Course not found");
        course.UpdatedBy = authService.Id!.Value;

        await courseRepository.Delete(course);

        return Unit.Value;
    }
}
