using MediatR;

namespace LabReserve.Application.UseCases.Groups.UpdateGroup;

public class UpdateGroupCommand : IRequest
{
    public string Name { get; set; }
    public long CourseId { get; set; }
    public long GroupId { get; set; }
}
