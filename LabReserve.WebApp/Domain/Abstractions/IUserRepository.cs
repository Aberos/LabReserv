using LabReserve.WebApp.Domain.Entities;

namespace LabReserve.WebApp.Domain.Abstractions;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetByEmail(string email);
}