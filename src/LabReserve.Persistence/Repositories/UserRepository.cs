using Dapper;
using LabReserve.Domain.Abstractions;
using LabReserve.Domain.Entities;
using LabReserve.Persistence.Abstractions;

namespace LabReserve.Persistence.Repositories;

public class UserRepository(IDbSession session) : BaseRepository<User>(session), IUserRepository
{
    protected override string Table => "users";

    protected override string Columns =>
        $"{Alias}.email, {Alias}.first_name, {Alias}.last_name, {Alias}.phone, {Alias}.user_type";

    protected override string SearchCondition =>
        $"{Alias}.email LIKE @Search OR {Alias}.first_name LIKE @Search OR {Alias}.last_name LIKE @Search OR {Alias}.phone LIKE @Search";

    protected override string OrderBy => $"{Alias}.email";

    public Task<long> Create(User entity)
    {
        return Insert(
            "email, first_name, last_name, phone, password, user_type",
            "@Email, @FirstName, @LastName, @Phone, @Password, @UserType",
            entity);
    }

    public Task Update(User entity)
    {
        return UpdateColumns(
            "email = @Email, first_name = @FirstName, last_name = @LastName, phone = @Phone, user_type = @UserType",
            entity);
    }

    public Task UpdatePassword(User entity)
    {
        return UpdateColumns("password = @Password", entity);
    }

    public Task<User?> GetByEmail(string email)
    {
        return QuerySingle($"{SelectColumns}, {Alias}.password", $"{Alias}.email = @Email", new { Email = email });
    }

    public Task AddGroup(GroupUser group)
    {
        return Session.Connection.ExecuteAsync("""
            UPDATE group_user SET
                status = @Active,
                updated_by = @CreatedBy,
                updated_date = GETDATE()
            WHERE id_user = @IdUser AND id_group = @IdGroup;

            IF @@ROWCOUNT = 0
                INSERT INTO group_user (status, id_user, id_group, created_by, created_date)
                VALUES (@Active, @IdUser, @IdGroup, @CreatedBy, GETDATE());
            """, WithStatusParameters(group), Session.Transaction);
    }

    public Task RemoveAllGroup(long userId, long updatedBy)
    {
        return Session.Connection.ExecuteAsync("""
            UPDATE group_user SET
                status = @Disabled,
                updated_by = @UpdatedBy,
                updated_date = GETDATE()
            WHERE id_user = @IdUser AND status = @Active
            """, WithStatusParameters(new { IdUser = userId, UpdatedBy = updatedBy }), Session.Transaction);
    }

    public Task<IEnumerable<GroupUser>> GetGroups(long userId)
    {
        return Session.Connection.QueryAsync<GroupUser>("""
            SELECT id_user, id_group, status, created_by, created_date, updated_by, updated_date
            FROM group_user
            WHERE id_user = @IdUser AND status = @Active
            """, WithStatusParameters(new { IdUser = userId }), Session.Transaction);
    }
}
