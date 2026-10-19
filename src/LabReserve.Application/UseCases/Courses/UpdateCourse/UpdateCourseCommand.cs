using MediatR;

namespace LabReserve.Application.UseCases.Courses.UpdateCourse;

public class UpdateCourseCommand : IRequest
{
    public string Name { get; set; }

    public long CourseId { get; set; }
}
