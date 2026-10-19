using Mediator;

namespace LabReserve.Application.UseCases.Courses.UpdateCourse;

public class UpdateCourseCommand : ICommand
{
    public string Name { get; set; }

    public long CourseId { get; set; }
}
