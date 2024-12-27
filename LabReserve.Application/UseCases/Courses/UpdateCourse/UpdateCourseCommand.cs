using MediatR;

namespace LabReserve.Application.UseCases.Courses.UpdateCourse;

public record UpdateCourseCommand(string Name, long CourseId) : IRequest
{

}
