using MediatR;

namespace LabReserve.Application.UseCases.Groups.UpdateGroup;

public record UpdateGroupCommand(string Name, long CourseId, long GroupId) : IRequest
{

}
