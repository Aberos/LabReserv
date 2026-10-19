using Mediator;

namespace LabReserve.Application.UseCases.Courses.DeleteCourse;

public record DeleteCourseCommand(long CourseId) : ICommand
{

}
