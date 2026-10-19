using LabReserve.Application.UseCases.Courses;
using LabReserve.Domain.Entities;

namespace LabReserve.Application.UseCases.Groups;

public record GroupResponse(long Id, string Name, long CourseId, CourseResponse? Course)
{
    public static GroupResponse From(Group group) =>
        new(group.Id, group.Name, group.CourseId, group.Course is null ? null : CourseResponse.From(group.Course));
}
