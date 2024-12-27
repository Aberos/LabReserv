using MediatR;

namespace LabReserve.Application.UseCases.Groups.CreateGroup;

public record CreateGroupCommand(string Name, long CourseId) : IRequest
{

}
