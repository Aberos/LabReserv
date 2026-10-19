using LabReserve.Domain.Entities;

namespace LabReserve.Application.UseCases.Courses;

public record CourseResponse(long Id, string Name)
{
    public static CourseResponse From(Course course) => new(course.Id, course.Name);
}
