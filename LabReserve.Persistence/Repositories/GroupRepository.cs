using Dapper;
using LabReserve.Domain.Abstractions;
using LabReserve.Domain.Dto;
using LabReserve.Domain.Entities;
using LabReserve.Persistence.Abstractions;

namespace LabReserve.Persistence.Repositories
{
    public class GroupRepository : IGroupRepository
    {
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
            var query = @"
                SELECT
                    g.id as Id,
                    g.status as Status,
                    g.name as Name,
                    g.updated_by as UpdatedBy,
                    g.updated_date as UpdatedDate,
                    g.created_by as CreatedBy,
                    g.created_date as CreatedDate,
                    c.id as CourseId,
                    c.name as CourseName,
                    c.status as CourseStatus,
                    c.updated_by as CourseUpdatedBy,
                    c.updated_date as CourseUpdatedDate,
                    c.created_by as CourseCreatedBy,
                    c.created_date as CourseCreatedDate
                FROM groups g
                LEFT JOIN courses c ON g.id_course = c.id
                WHERE g.id = @Id AND g.status = 1";

            var result = await _session.Connection.QueryAsync<Group, Course, Group>(query, (group, course) =>
            {
                group.Course = course;
                return group;
            }, new { Id = id }, _session.Transaction, splitOn: "CourseId");

            return result?.FirstOrDefault()!;
        }

        public async Task<Group> GetByName(string name)
        {
            var query = @"
                SELECT
                    g.id as Id,
                    g.status as Status,
                    g.name as Name,
                    g.updated_by as UpdatedBy,
                    g.updated_date as UpdatedDate,
                    g.created_by as CreatedBy,
                    g.created_date as CreatedDate,
                    c.id as CourseId,
                    c.name as CourseName,
                    c.status as CourseStatus,
                    c.updated_by as CourseUpdatedBy,
                    c.updated_date as CourseUpdatedDate,
                    c.created_by as CourseCreatedBy,
                    c.created_date as CourseCreatedDate
                FROM groups g
                LEFT JOIN courses c ON g.id_course = c.id
                WHERE TRIM(g.name) = TRIM(@Name) AND g.status = 1";

            var result = await _session.Connection.QueryAsync<Group, Course, Group>(query, (group, course) =>
            {
                group.Course = course;
                return group;
            }, new { Name = name }, _session.Transaction, splitOn: "CourseId");

            return result!.FirstOrDefault()!;
        }

        public async Task<FilterResponseDto<Group>> GetFilteredList(FilterRequestDto filter)
        {
            var search = string.IsNullOrWhiteSpace(filter?.Search) ? null : $"%{filter.Search}%";
            var requestPage = filter?.Page ?? 1;
            var size = filter?.PageSize ?? 10;
            var skip = (requestPage - 1) * size;
            var requestId = filter?.RequestId ?? 1;

            var query = @"
                SELECT
                    g.id as Id,
                    g.status as Status,
                    g.name as Name,
                    g.updated_by as UpdatedBy,
                    g.updated_date as UpdatedDate,
                    g.created_by as CreatedBy,
                    g.created_date as CreatedDate,
                    c.id as CourseId,
                    c.name as CourseName,
                    c.status as CourseStatus,
                    c.updated_by as CourseUpdatedBy,
                    c.updated_date as CourseUpdatedDate,
                    c.created_by as CourseCreatedBy,
                    c.created_date as CourseCreatedDate
                FROM groups g
                LEFT JOIN courses c ON g.id_course = c.id
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

            using var multi = await _session.Connection.QueryMultipleAsync(query, new { Search = search, Skip = skip, Size = size }, _session.Transaction);
            var groups = multi.Read<Group, Course, Group>((group, course) =>
            {
                group.Course = course;
                return group;
            }, splitOn: "CourseId").ToList();

            var totalCount = multi.ReadFirst<int>();

            var totalPages = (int)Math.Ceiling(totalCount / (double)size);
            var currentPage = (skip / size) + 1;

            return new FilterResponseDto<Group>
            {
                Data = groups,
                TotalCount = totalCount,
                Page = currentPage,
                TotalPages = totalPages,
                RequestId = requestId
            };
        }

        public async Task<IEnumerable<Group>> GetByList(List<long> ids)
        {
            var query = @"
                SELECT
                    g.id as Id,
                    g.status as Status,
                    g.name as Name,
                    g.updated_by as UpdatedBy,
                    g.updated_date as UpdatedDate,
                    g.created_by as CreatedBy,
                    g.created_date as CreatedDate,
                    c.id as CourseId,
                    c.name as CourseName,
                    c.status as CourseStatus,
                    c.updated_by as CourseUpdatedBy,
                    c.updated_date as CourseUpdatedDate,
                    c.created_by as CourseCreatedBy,
                    c.created_date as CourseCreatedDate
                FROM groups g
                LEFT JOIN courses c ON g.id_course = c.id
                WHERE g.id IN @Ids AND g.status = 1";

            var result = await _session.Connection.QueryAsync<Group, Course, Group>(query, (group, course) =>
            {
                group.Course = course;
                return group;
            }, new { Ids = ids }, _session.Transaction, splitOn: "CourseId");

            return result;
        }
    }
}
