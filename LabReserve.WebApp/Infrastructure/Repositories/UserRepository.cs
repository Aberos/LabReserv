using Dapper;
using LabReserve.WebApp.Domain.Abstractions;
using LabReserve.WebApp.Domain.Dto;
using LabReserve.WebApp.Domain.Entities;
using LabReserve.WebApp.Infrastructure.Abstractions;

namespace LabReserve.WebApp.Infrastructure.Repositories;

public class UserRepository :  IUserRepository
{
    private readonly IDbSession _session;

    public UserRepository(IDbSession session)
    {
        _session = session;
    }
    
    public void Create(User entity)
    {
        _session.Connection.Execute("", null, _session.Transaction);
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
       return _session.Connection.QueryFirstAsync<User>("", null, _session.Transaction);
    }

    public Task<IEnumerable<User>> GetAll(FilterRequest filter)
    {
        return _session.Connection.QueryAsync<User>("", null, _session.Transaction);
    }

    public Task<User?> GetByEmail(string email)
    {
        return _session.Connection.QueryFirstAsync<User>("", null, _session.Transaction)!;
    }
}