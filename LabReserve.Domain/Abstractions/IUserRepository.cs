using LabReserve.Domain.Entities;

namespace LabReserve.Domain.Abstractions;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetByEmail(string email);
}