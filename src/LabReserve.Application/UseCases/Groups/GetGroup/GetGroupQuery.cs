using Mediator;

namespace LabReserve.Application.UseCases.Groups.GetGroup;

public record GetGroupQuery(long GroupId) : IQuery<GroupResponse?>;
