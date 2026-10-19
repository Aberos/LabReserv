using LabReserve.Domain.Abstractions;
using LabReserve.Domain.Dto;
using Mediator;

namespace LabReserve.Application.UseCases.Groups.GetGroups;

public class GetGroupsHandler(IGroupRepository groupRepository) : IQueryHandler<GetGroupsQuery, FilterResponseDto<GroupResponse>>
{
    public async ValueTask<FilterResponseDto<GroupResponse>> Handle(GetGroupsQuery request, CancellationToken cancellationToken)
    {
        var page = await groupRepository.GetFilteredList(request);
        return page.Map(GroupResponse.From);
    }
}
