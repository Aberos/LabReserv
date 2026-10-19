using LabReserve.Domain.Abstractions;
using LabReserve.Domain.Dto;
using LabReserve.Domain.Entities;
using Mediator;

namespace LabReserve.Application.UseCases.Courses.GetCourses;

public class GetCoursesHandler : IQueryHandler<GetCoursesQuery, FilterResponseDto<Course>>
{
    private readonly ICourseRepository _courseRepository;
    public GetCoursesHandler(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async ValueTask<FilterResponseDto<Course>> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
    {
        return await _courseRepository.GetFilteredList(request);
    }
}
