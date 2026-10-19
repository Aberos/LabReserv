using LabReserve.Domain.Abstractions;
using LabReserve.Domain.Dto;
using LabReserve.Domain.Entities;
using Mediator;

namespace LabReserve.Application.UseCases.Groups.GetGroups;

public class GetGroupsHandler : IQueryHandler<GetGroupsQuery, FilterResponseDto<Group>>
{
    private readonly IGroupRepository _groupRepository;
    public GetGroupsHandler(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async ValueTask<FilterResponseDto<Group>> Handle(GetGroupsQuery request, CancellationToken cancellationToken)
    {
        return await _groupRepository.GetFilteredList(request);
    }
}
