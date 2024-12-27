using LabReserve.Domain.Abstractions;
using LabReserve.Domain.Entities;
using MediatR;

namespace LabReserve.Application.UseCases.Groups.GetGroup;

public class GetGroupHandler : IRequestHandler<GetGroupQuery, Group>
{
    private readonly IGroupRepository _groupRepository;
    public GetGroupHandler(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public Task<Group> Handle(GetGroupQuery request, CancellationToken cancellationToken)
    {
        return _groupRepository.Get(request.GroupId);
    }
}
