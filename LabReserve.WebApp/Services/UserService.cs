using LabReserve.WebApp.Domain.Abstractions;
using LabReserve.WebApp.Domain.Dto;
using LabReserve.WebApp.Domain.Entities;

namespace LabReserve.WebApp.Services;

public class UserService : IUserService
{
    public void Create(User entity)
    {
        throw new NotImplementedException();
    }

    public void Update(User entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(User entity)
    {
        throw new NotImplementedException();
    }

    public Task<User> Get(long id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>> GetAll(FilterRequest filter)
    {
        throw new NotImplementedException();
    }

    public Task<UserAuthDto> SignIn(string email, string password)
    {
        throw new NotImplementedException();
    }
}