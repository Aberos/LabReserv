using LabReserve.Domain.Abstractions;
using LabReserve.Domain.Dto;
using LabReserve.Domain.Entities;
using MediatR;

namespace LabReserve.Application.UseCases.Groups.GetGroups;

public class GetGroupsHandler : IRequestHandler<GetGroupsQuery, FilterResponseDto<Group>>
{
    private readonly IGroupRepository _groupRepository;
    public GetGroupsHandler(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public Task<FilterResponseDto<Group>> Handle(GetGroupsQuery request, CancellationToken cancellationToken)
    {
        return _groupRepository.GetFilteredList(request);
    }
}
