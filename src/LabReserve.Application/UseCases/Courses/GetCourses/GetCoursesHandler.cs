using LabReserve.Domain.Abstractions;
using LabReserve.Domain.Dto;
using Mediator;

namespace LabReserve.Application.UseCases.Courses.GetCourses;

public class GetCoursesHandler(ICourseRepository courseRepository) : IQueryHandler<GetCoursesQuery, FilterResponseDto<CourseResponse>>
{
    public async ValueTask<FilterResponseDto<CourseResponse>> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
    {
        var page = await courseRepository.GetFilteredList(request);
        return page.Map(CourseResponse.From);
    }
}
