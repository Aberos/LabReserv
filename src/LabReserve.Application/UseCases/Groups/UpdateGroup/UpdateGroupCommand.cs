using Mediator;

namespace LabReserve.Application.UseCases.Groups.UpdateGroup;

public class UpdateGroupCommand : ICommand
{
    public string Name { get; set; }
    public long CourseId { get; set; }
    public long GroupId { get; set; }
}
