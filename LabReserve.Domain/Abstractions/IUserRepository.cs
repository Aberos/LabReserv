using LabReserve.Domain.Entities;

namespace LabReserve.Domain.Abstractions;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetByEmail(string email);
    Task AddGroup(GroupUser group);
    Task RemoveGroup(GroupUser group);
    Task RemoveAllGroup(long userId, string updatedBy);
    Task<IEnumerable<GroupUser>> GetGroups(long userId);
    Task<bool> AnyGroup(long userId, long groupId);
}