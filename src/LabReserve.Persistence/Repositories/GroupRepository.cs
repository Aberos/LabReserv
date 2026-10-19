using Dapper;
using LabReserve.Domain.Abstractions;
using LabReserve.Domain.Dto;
using LabReserve.Domain.Entities;
using LabReserve.Persistence.Abstractions;

namespace LabReserve.Persistence.Repositories;

public class GroupRepository : IGroupRepository
{
    private const string CourseSplitColumn = "Id";

    private const string SelectGroupWithCourse = @"
        SELECT
            g.id as Id,
            g.status as Status,
            g.name as Name,
            g.id_course as CourseId,
            g.updated_by as UpdatedBy,
            g.updated_date as UpdatedDate,
            g.created_by as CreatedBy,
            g.created_date as CreatedDate,
            c.id as Id,
            c.name as Name,
            c.status as Status,
            c.updated_by as UpdatedBy,
            c.updated_date as UpdatedDate,
            c.created_by as CreatedBy,
            c.created_date as CreatedDate
        FROM groups g
        LEFT JOIN courses c ON g.id_course = c.id";

    private readonly IDbSession _session;

    public GroupRepository(IDbSession session)
    {
        _session = session;
    }

    public Task<long> Create(Group entity)
    {
        return _session.Connection.ExecuteScalarAsync<long>(@"INSERT INTO groups (status, name, id_course, created_by, created_date)
            OUTPUT INSERTED.id
            VALUES (1, @Name, @CourseId, @CreatedBy, GETDATE())", entity, _session.Transaction);
    }

    public Task Update(Group entity)
    {
        return _session.Connection.ExecuteAsync(@"UPDATE groups SET
                name = @Name,
                id_course = @CourseId,
                updated_by = @UpdatedBy,
                updated_date = GETDATE()
            WHERE
                id = @Id AND status = 1",
            entity, _session.Transaction);
    }

    public Task Delete(Group entity)
    {
        return _session.Connection.ExecuteAsync(@"UPDATE groups SET
                status = 2,
                updated_by = @UpdatedBy,
                updated_date = GETDATE()
            WHERE id = @Id AND status = 1", entity, _session.Transaction);
    }

    public async Task<Group> Get(long id)
    {
        var groups = await QueryGroupsWithCourse(
            $"{SelectGroupWithCourse} WHERE g.id = @Id AND g.status = 1",
            new { Id = id });

        return groups.FirstOrDefault()!;
    }

    public async Task<Group> GetByName(string name)
    {
        var groups = await QueryGroupsWithCourse(
            $"{SelectGroupWithCourse} WHERE TRIM(g.name) = TRIM(@Name) AND g.status = 1",
            new { Name = name });

        return groups.FirstOrDefault()!;
    }

    public Task<IEnumerable<Group>> GetByList(List<long> ids)
    {
        return QueryGroupsWithCourse(
            $"{SelectGroupWithCourse} WHERE g.id IN @Ids AND g.status = 1",
            new { Ids = ids });
    }

    public async Task<FilterResponseDto<Group>> GetFilteredList(FilterRequestDto filter)
    {
        var query = $@"
            {SelectGroupWithCourse}
            WHERE
                g.status = 1 AND
                (@Search IS NULL OR g.name LIKE @Search)
            ORDER BY g.name
            OFFSET @Skip ROWS FETCH NEXT @Size ROWS ONLY;

            SELECT COUNT(*)
            FROM groups g
            WHERE
                g.status = 1 AND
                (@Search IS NULL OR g.name LIKE @Search);";

        using var multi = await _session.Connection.QueryMultipleAsync(
            query,
            new { Search = filter.SearchPattern, filter.Skip, Size = filter.CurrentPageSize },
            _session.Transaction);

        var groups = multi.Read<Group, Course, Group>(AttachCourse, splitOn: CourseSplitColumn).ToList();
        var totalCount = multi.ReadFirst<int>();

        return FilterResponseDto<Group>.Create(groups, totalCount, filter);
    }

    private Task<IEnumerable<Group>> QueryGroupsWithCourse(string query, object parameters)
    {
        return _session.Connection.QueryAsync<Group, Course, Group>(
            query, AttachCourse, parameters, _session.Transaction, splitOn: CourseSplitColumn);
    }

    private static Group AttachCourse(Group group, Course course)
    {
        group.Course = course;
        return group;
    }
}
