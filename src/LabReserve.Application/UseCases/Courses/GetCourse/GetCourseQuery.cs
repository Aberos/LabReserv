using LabReserve.Domain.Entities;
using Mediator;

namespace LabReserve.Application.UseCases.Courses.GetCourse;

public record GetCourseQuery(long CourseId) : IQuery<Course>
{

}
