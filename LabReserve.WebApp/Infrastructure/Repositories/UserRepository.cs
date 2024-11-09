using Dapper;
using LabReserve.WebApp.Domain.Abstractions;
using LabReserve.WebApp.Domain.Dto;
using LabReserve.WebApp.Domain.Entities;
using LabReserve.WebApp.Domain.Enums;
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
        _session.Connection.Execute(@"INSERT INTO users 
                (status, email, first_name, last_name, phone, password, user_type, created_by, created_date)
                VALUES (1, @Email, @FirstName, @LastName, @Phone, @Password, @UserType, @CreatedBy, GETDATE())", entity, _session.Transaction);
    }

    public void Update(User entity)
    {
        _session.Connection.Execute(@"UPDATE users SET
                 email = @Email,
                 first_name = @FirstName,
                 last_name = @LastName,
                 phone = @Phone,
                 password = @Password,
                 user_type = @UserType,
                 updated_by = @UpdatedBy,
                 updated_date = GETDATE()
                WHERE
                    id = @Id AND status = 1
                 ", entity, _session.Transaction);
    }

    public void Delete(User entity)
    {
        _session.Connection.Execute(@"UPDATE users SET 
                 status = 2,
                 updated_by = @UpdatedBy,
                 updated_date = GETDATE()
            WHERE id = @Id AND status = 1", entity, _session.Transaction);
    }

    public Task<User> Get(long id)
    {
       return _session.Connection.QueryFirstOrDefaultAsync<User>(@"SELECT
                u.id as Id,
                u.status as Status,
                u.email as Email,
                u.first_name as FirstName,
                u.last_name as LastName,
                u.phone as Phone,
                u.password as Password,
                u.user_type as UserType,
                u.updated_by as UpdatedBy,
                u.updated_date as UpdatedDate,
                u.created_by as CreatedBy,
                u.created_date as CreatedDate
            FROM users u
            WHERE u.id = @Id AND u.status = 1", new {Id = id}, _session.Transaction)!;
    }

    public Task<IEnumerable<User>> GetAll(FilterRequest filter)
    {
        return _session.Connection.QueryAsync<User>(@"SELECT
                u.id as Id,
                u.status as Status,
                u.email as Email,
                u.first_name as FirstName,
                u.last_name as LastName,
                u.phone as Phone,
                u.password as Password,
                u.user_type as UserType,
                u.updated_by as UpdatedBy,
                u.updated_date as UpdatedDate,
                u.created_by as CreatedBy,
                u.created_date as CreatedDate
            FROM users u
            WHERE 
                u.status = 1 AND
                (@Search is null OR u.email like @Search OR u.first_name like @Search OR u.last_name like @Search OR u.phone like @Search)", 
            new {
                    Search = string.IsNullOrWhiteSpace(filter?.Search) ? $"%{filter.Search}%" : null,
                    Skip = filter?.Skip ?? 0,
                    Size = filter?.Size ?? 1,
                }, _session.Transaction);
    }

    public Task<User?> GetByEmail(string email)
    {
        return _session.Connection.QueryFirstOrDefaultAsync<User>(@"SELECT
                u.id as Id,
                u.status as Status,
                u.email as Email,
                u.first_name as FirstName,
                u.last_name as LastName,
                u.phone as Phone,
                u.password as Password,
                u.user_type as UserType,
                u.updated_by as UpdatedBy,
                u.updated_date as UpdatedDate,
                u.created_by as CreatedBy,
                u.created_date as CreatedDate
            FROM users u
            WHERE 
                u.status = 1 AND u.email = @Email", new {Email = email}, _session.Transaction)!;
    }
}