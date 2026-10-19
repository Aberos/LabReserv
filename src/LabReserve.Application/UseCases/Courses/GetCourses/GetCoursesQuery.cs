using LabReserve.Domain.Dto;
using LabReserve.Domain.Entities;
using Mediator;

namespace LabReserve.Application.UseCases.Courses.GetCourses;

public class GetCoursesQuery : FilterRequestDto, IQuery<FilterResponseDto<Course>>
{

}
