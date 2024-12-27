using LabReserve.Domain.Entities;
using MediatR;

namespace LabReserve.Application.UseCases.Groups.GetGroup;

public record GetGroupQuery(long GroupId) : IRequest<Group>
{

}
