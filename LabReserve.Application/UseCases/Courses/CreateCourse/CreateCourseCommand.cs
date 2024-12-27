using MediatR;

namespace LabReserve.Application.UseCases.Courses.CreateCourse;

public record CreateCourseCommand(string Name) : IRequest
{

}
