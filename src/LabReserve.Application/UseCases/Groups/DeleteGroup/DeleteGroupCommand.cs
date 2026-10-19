using Mediator;

namespace LabReserve.Application.UseCases.Groups.DeleteGroup;

public record DeleteGroupCommand(long GroupId) : ICommand
{

}
