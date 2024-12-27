using MediatR;

namespace LabReserve.Application.UseCases.Groups.DeleteGroup;

public record DeleteGroupCommand(long GroupId) : IRequest
{

}
