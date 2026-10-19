using Dapper;
using LabReserve.Domain.Abstractions;
using LabReserve.Domain.Entities;
using LabReserve.Persistence.Abstractions;

namespace LabReserve.Persistence.Repositories;

public class GroupRepository(IDbSession session) : NamedRepository<Group>(session), IGroupRepository
{
    private const string CourseAlias = "c";
    private const string CourseSplitColumn = "id";

    protected override string Table => "groups";

    protected override string Columns => $"{base.Columns}, {Alias}.id_course AS course_id";

    protected override string Source => $"{base.Source} LEFT JOIN courses {CourseAlias} ON {CourseAlias}.id = {Alias}.id_course";

    protected override string SelectColumns => $"""
        {base.SelectColumns},
        {CourseAlias}.id, {CourseAlias}.name, {CourseAlias}.status,
        {CourseAlias}.created_by, {CourseAlias}.created_date, {CourseAlias}.updated_by, {CourseAlias}.updated_date
        """;

    public Task<long> Create(Group entity)
    {
        return Insert("name, id_course", "@Name, @CourseId", entity);
    }

    public Task Update(Group entity)
    {
        return UpdateColumns("name = @Name, id_course = @CourseId", entity);
    }

    public Task<IEnumerable<Group>> GetByList(List<long> ids)
    {
        return QueryWhere($"{Alias}.id IN @Ids", new { Ids = ids });
    }

    protected override Task<IEnumerable<Group>> Query(string sql, DynamicParameters parameters)
    {
        return Session.Connection.QueryAsync<Group, Course, Group>(
            sql, AttachCourse, parameters, Session.Transaction, splitOn: CourseSplitColumn);
    }

    protected override IEnumerable<Group> Read(SqlMapper.GridReader reader)
    {
        return reader.Read<Group, Course, Group>(AttachCourse, splitOn: CourseSplitColumn);
    }

    private static Group AttachCourse(Group group, Course course)
    {
        group.Course = course;
        return group;
    }
}
