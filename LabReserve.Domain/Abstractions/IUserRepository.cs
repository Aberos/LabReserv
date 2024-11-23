using LabReserve.Domain.Entities;

namespace LabReserve.Domain.Abstractions;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetByEmail(string email);
    void AddGroup(GroupUser group);
    void RemoveGroup(GroupUser group);
    void RemoveAllGroup(long userId, string updatedBy);
    Task<IEnumerable<GroupUser>> GetGroups(long userId);
    Task<bool> AnyGroup(long userId, long groupId);
}