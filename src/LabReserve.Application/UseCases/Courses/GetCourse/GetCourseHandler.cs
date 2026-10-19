using LabReserve.Domain.Abstractions;
using LabReserve.Domain.Entities;
using Mediator;

namespace LabReserve.Application.UseCases.Courses.GetCourse;

public class GetCourseHandler : IQueryHandler<GetCourseQuery, Course?>
{
    private readonly ICourseRepository _courseRepository;
    public GetCourseHandler(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async ValueTask<Course?> Handle(GetCourseQuery request, CancellationToken cancellationToken)
    {
        return await _courseRepository.Get(request.CourseId);
    }
}
