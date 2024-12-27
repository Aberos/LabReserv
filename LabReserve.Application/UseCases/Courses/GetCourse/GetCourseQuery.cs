using LabReserve.Domain.Entities;
using MediatR;

namespace LabReserve.Application.UseCases.Courses.GetCourse;

public record GetCourseQuery(long CourseId) : IRequest<Course>
{

}
