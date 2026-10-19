using LabReserve.Domain.Abstractions;
using Mediator;

namespace LabReserve.Application.UseCases.Groups.GetGroup;

public class GetGroupHandler(IGroupRepository groupRepository) : IQueryHandler<GetGroupQuery, GroupResponse?>
{
    public async ValueTask<GroupResponse?> Handle(GetGroupQuery request, CancellationToken cancellationToken)
    {
        var group = await groupRepository.Get(request.GroupId);
        return group is null ? null : GroupResponse.From(group);
    }
}
