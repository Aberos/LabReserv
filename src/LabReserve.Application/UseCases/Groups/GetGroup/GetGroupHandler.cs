using LabReserve.Domain.Abstractions;
using LabReserve.Domain.Entities;
using Mediator;

namespace LabReserve.Application.UseCases.Groups.GetGroup;

public class GetGroupHandler : IQueryHandler<GetGroupQuery, Group>
{
    private readonly IGroupRepository _groupRepository;
    public GetGroupHandler(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async ValueTask<Group> Handle(GetGroupQuery request, CancellationToken cancellationToken)
    {
        return await _groupRepository.Get(request.GroupId);
    }
}
