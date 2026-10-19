using LabReserve.Domain.Abstractions;
using Mediator;

namespace LabReserve.Application.UseCases.Users.GetUser;

public class GetUserHandler(IUserRepository userRepository, IGroupRepository groupRepository)
    : IQueryHandler<GetUserQuery, UserDetailsResponse?>
{
    public async ValueTask<UserDetailsResponse?> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.Get(request.Id);
        if (user is null)
            return null;

        var groupIds = (await userRepository.GetGroups(user.Id)).Select(link => link.IdGroup).ToList();
        var groups = groupIds.Count == 0 ? [] : await groupRepository.GetByList(groupIds);

        return UserDetailsResponse.From(user, groups);
    }
}
