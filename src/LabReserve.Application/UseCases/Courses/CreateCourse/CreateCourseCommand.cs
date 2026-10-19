using Mediator;

namespace LabReserve.Application.UseCases.Courses.CreateCourse;

public record CreateCourseCommand(string Name) : ICommand
{

}
