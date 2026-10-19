using LabReserve.Domain.Abstractions;
using LabReserve.Domain.Entities;
using MediatR;

namespace LabReserve.Application.UseCases.Courses.GetCourse;

public class GetCourseHandler : IRequestHandler<GetCourseQuery, Course>
{
    private readonly ICourseRepository _courseRepository;
    public GetCourseHandler(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public Task<Course> Handle(GetCourseQuery request, CancellationToken cancellationToken)
    {
        return _courseRepository.Get(request.CourseId);
    }
}
