using Dapper;
using LabReserve.Domain.Abstractions;
using LabReserve.Domain.Dto;
using LabReserve.Domain.Entities;
using LabReserve.Persistence.Abstractions;
using static Dapper.SqlMapper;

namespace LabReserve.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IDbSession _session;

    public UserRepository(IDbSession session)
    {
        _session = session;
    }

    public Task<long> Create(User entity)
    {
        return _session.Connection.ExecuteScalarAsync<long>(@"INSERT INTO users 
                (status, email, first_name, last_name, phone, password, user_type, created_by, created_date)
                VALUES (1, @Email, @FirstName, @LastName, @Phone, @Password, @UserType, @CreatedBy, GETDATE()) RETURNING id", entity, _session.Transaction);
    }

    public Task Update(User entity)
    {
        return _session.Connection.ExecuteAsync(@"UPDATE users SET
                 email = @Email,
                 first_name = @FirstName,
                 last_name = @LastName,
                 phone = @Phone,
                 user_type = @UserType,
                 updated_by = @UpdatedBy,
                 updated_date = GETDATE()
                WHERE
                    id = @Id AND status = 1
                 ", entity, _session.Transaction);
    }

    public Task UpdatePassword(User entity)
    {
        return _session.Connection.ExecuteAsync(@"UPDATE users SET
                 password = @Password,
                 user_type = @UserType,
                 updated_by = @UpdatedBy,
                 updated_date = GETDATE()
                WHERE
                    id = @Id AND status = 1
                 ", entity, _session.Transaction);
    }

    public Task Delete(User entity)
    {
        return _session.Connection.ExecuteAsync(@"UPDATE users SET 
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
            WHERE u.id = @Id AND u.status = 1", new { Id = id }, _session.Transaction)!;
    }

    public Task<IEnumerable<User>> GetAll(FilterRequestDto filter)
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
            new
            {
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
                u.status = 1 AND u.email = @Email", new { Email = email }, _session.Transaction)!;
    }

    public Task AddGroup(GroupUser group)
    {
        return _session.Connection.ExecuteAsync(@"INSERT INTO group_user (status, id_user, id_group, created_by, created_date)
                VALUES (1, @IdUser, @IdGroup, @CreatedBy, GETDATE())", group, _session.Transaction);
    }

    public Task RemoveGroup(GroupUser group)
    {
        return _session.Connection.ExecuteAsync(@"UPDATE users SET 
                 status = 2,
                 updated_by = @UpdatedBy,
                 updated_date = GETDATE()
            WHERE id_user = @IdUser AND id_group = @IdGroup AND status = 1", group, _session.Transaction);
    }

    public Task RemoveAllGroup(long userId, long updatedBy)
    {
        return _session.Connection.ExecuteAsync(@"UPDATE users SET 
                 status = 2,
                 updated_by = @UpdatedBy,
                 updated_date = GETDATE()
            WHERE id_user = @IdUser AND status = 1", new { IdUser = userId, UpdatedBy = updatedBy }, _session.Transaction);
    }

    public Task<IEnumerable<GroupUser>> GetGroups(long userId)
    {
        return _session.Connection.QueryAsync<GroupUser>(@"SELECT
                g.id_user as IdUser,
                g.id_group as IdGroup,
                g.status as Status,
                g.updated_by as UpdatedBy,
                g.updated_date as UpdatedDate,
                g.created_by as CreatedBy,
                g.created_date as CreatedDate
            FROM group_user g
            WHERE g.id_user = @UserId AND g.status = 1", new { UserId = userId }, _session.Transaction)!;
    }

    public async Task<bool> AnyGroup(long userId, long groupId)
    {
        var groups = await _session.Connection.QueryAsync<GroupUser>(@"SELECT
                g.id_user as IdUser,
            FROM group_user g
            WHERE g.id_user = @UserId AND g.id_group = @GroupId AND g.status = 1", new { UserId = userId, GroupId = groupId }, _session.Transaction);

        return groups?.Any() ?? false;
    }
}