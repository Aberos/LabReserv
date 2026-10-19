using LabReserve.Domain.Entities;

namespace LabReserve.Domain.Abstractions;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetByEmail(string email);
    Task UpdatePassword(User entity);
    Task AddGroup(GroupUser group);
    Task RemoveAllGroup(long userId, long updatedBy);
    Task<IEnumerable<GroupUser>> GetGroups(long userId);
}
