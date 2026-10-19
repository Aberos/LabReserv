using LabReserve.Domain.Abstractions;
using LabReserve.Domain.Dto;
using LabReserve.Domain.Entities;
using MediatR;

namespace LabReserve.Application.UseCases.Courses.GetCourses;

public class GetCoursesHandler : IRequestHandler<GetCoursesQuery, FilterResponseDto<Course>>
{
    private readonly ICourseRepository _courseRepository;
    public GetCoursesHandler(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public Task<FilterResponseDto<Course>> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
    {
        return _courseRepository.GetFilteredList(request);
    }
}
