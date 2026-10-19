using Dapper;
using LabReserve.Domain.Dto;
using LabReserve.Domain.Entities;
using LabReserve.Domain.Enums;
using LabReserve.Persistence.Abstractions;

namespace LabReserve.Persistence.Repositories;

public abstract class BaseRepository<T>(IDbSession session) where T : BaseEntity
{
    protected const string Alias = "e";

    private const string AuditColumns =
        $"{Alias}.status, {Alias}.created_by, {Alias}.created_date, {Alias}.updated_by, {Alias}.updated_date";

    private const string ActiveCondition = $"{Alias}.status = @Active";

    protected IDbSession Session { get; } = session;

    protected abstract string Table { get; }

    protected abstract string Columns { get; }

    protected abstract string SearchCondition { get; }

    protected abstract string OrderBy { get; }

    protected virtual string Source => $"{Table} {Alias}";

    protected virtual string SelectColumns => $"{Alias}.id, {Columns}, {AuditColumns}";

    public Task<T?> Get(long id)
    {
        return QuerySingle($"{Alias}.id = @Id", new { Id = id });
    }

    public Task Delete(T entity)
    {
        return ExecuteOnActive($"""
            UPDATE {Table} SET
                status = @Disabled,
                updated_by = @UpdatedBy,
                updated_date = GETDATE()
            WHERE id = @Id AND status = @Active
            """, WithStatusParameters(entity));
    }

    public async Task<FilterResponseDto<T>> GetFilteredList(FilterRequestDto filter)
    {
        var where = $"WHERE {ActiveCondition} AND (@Search IS NULL OR {SearchCondition})";
        var sql = $"""
            SELECT {SelectColumns}
            FROM {Source}
            {where}
            ORDER BY {OrderBy}
            OFFSET @Skip ROWS FETCH NEXT @Size ROWS ONLY;

            SELECT COUNT(*)
            FROM {Source}
            {where};
            """;

        var parameters = WithStatusParameters(new { Search = filter.SearchPattern, filter.Skip, Size = filter.CurrentPageSize });
        using var reader = await Session.Connection.QueryMultipleAsync(sql, parameters, Session.Transaction);

        var data = Read(reader).ToList();
        var totalCount = await reader.ReadFirstAsync<int>();

        return FilterResponseDto<T>.Create(data, totalCount, filter);
    }

    protected Task<long> Insert(string columns, string values, T entity)
    {
        return Session.Connection.ExecuteScalarAsync<long>($"""
            INSERT INTO {Table} ({columns}, status, created_by, created_date)
            OUTPUT INSERTED.id
            VALUES ({values}, @Active, @CreatedBy, GETDATE())
            """, WithStatusParameters(entity), Session.Transaction);
    }

    protected Task UpdateColumns(string assignments, T entity)
    {
        return ExecuteOnActive($"""
            UPDATE {Table} SET
                {assignments},
                updated_by = @UpdatedBy,
                updated_date = GETDATE()
            WHERE id = @Id AND status = @Active
            """, WithStatusParameters(entity));
    }

    protected Task<T?> QuerySingle(string condition, object parameters)
    {
        return QuerySingle(SelectColumns, condition, parameters);
    }

    protected async Task<T?> QuerySingle(string columns, string condition, object parameters)
    {
        var result = await Query($"SELECT {columns} FROM {Source} WHERE {ActiveCondition} AND {condition}", WithStatusParameters(parameters));
        return result.FirstOrDefault();
    }

    protected Task<IEnumerable<T>> QueryWhere(string condition, object parameters)
    {
        return Query($"SELECT {SelectColumns} FROM {Source} WHERE {ActiveCondition} AND {condition}", WithStatusParameters(parameters));
    }

    protected virtual Task<IEnumerable<T>> Query(string sql, DynamicParameters parameters)
    {
        return Session.Connection.QueryAsync<T>(sql, parameters, Session.Transaction);
    }

    protected virtual IEnumerable<T> Read(SqlMapper.GridReader reader)
    {
        return reader.Read<T>();
    }

    protected static DynamicParameters WithStatusParameters(object parameters)
    {
        var dynamicParameters = new DynamicParameters(parameters);
        dynamicParameters.Add("Active", Status.Enable);
        dynamicParameters.Add("Disabled", Status.Disable);
        return dynamicParameters;
    }

    private async Task ExecuteOnActive(string sql, DynamicParameters parameters)
    {
        var affectedRows = await Session.Connection.ExecuteAsync(sql, parameters, Session.Transaction);
        if (affectedRows == 0)
            throw new KeyNotFoundException($"{typeof(T).Name} not found");
    }
}
