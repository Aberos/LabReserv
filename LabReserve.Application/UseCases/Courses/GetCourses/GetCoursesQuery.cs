using LabReserve.Domain.Dto;
using LabReserve.Domain.Entities;
using MediatR;

namespace LabReserve.Application.UseCases.Courses.GetCourses;

public class GetCoursesQuery : FilterRequestDto, IRequest<IEnumerable<Course>>
{

}
