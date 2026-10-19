using LabReserve.Domain.Abstractions;
using Mediator;

namespace LabReserve.Application.UseCases.Courses.GetCourse;

public class GetCourseHandler(ICourseRepository courseRepository) : IQueryHandler<GetCourseQuery, CourseResponse?>
{
    public async ValueTask<CourseResponse?> Handle(GetCourseQuery request, CancellationToken cancellationToken)
    {
        var course = await courseRepository.Get(request.CourseId);
        return course is null ? null : CourseResponse.From(course);
    }
}
