using LabReserve.Domain.Abstractions;
using LabReserve.Domain.Entities;
using MediatR;

namespace LabReserve.Application.UseCases.Groups.GetGroups;

public class GetGroupsHandler : IRequestHandler<GetGroupsQuery, IEnumerable<Group>>
{
    private readonly IGroupRepository _groupRepository;
    public GetGroupsHandler(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public Task<IEnumerable<Group>> Handle(GetGroupsQuery request, CancellationToken cancellationToken)
    {
        return _groupRepository.GetAll(request);
    }
}
