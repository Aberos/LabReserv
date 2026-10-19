using LabReserve.Domain.Entities;
using LabReserve.Domain.Enums;

namespace LabReserve.Application.UseCases.Users;

public record UserGroupResponse(long Id, string Name);

public record UserDetailsResponse(
    long Id,
    string Email,
    string FirstName,
    string LastName,
    string Phone,
    UserType UserType,
    IEnumerable<UserGroupResponse> Groups)
{
    public static UserDetailsResponse From(User user, IEnumerable<Group> groups) => new(
        user.Id,
        user.Email,
        user.FirstName,
        user.LastName,
        user.Phone,
        user.UserType,
        groups.Select(group => new UserGroupResponse(group.Id, group.Name)).ToList());
}
