using LabReserve.Domain.Dto;
using Mediator;

namespace LabReserve.Application.UseCases.Courses.GetCourses;

public class GetCoursesQuery : FilterRequestDto, IQuery<FilterResponseDto<CourseResponse>>;
