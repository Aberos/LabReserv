using Dapper;
using LabReserve.Domain.Abstractions;
using LabReserve.Domain.Dto;
using LabReserve.Domain.Entities;
using LabReserve.Persistence.Abstractions;

namespace LabReserve.Persistence.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly IDbSession _session;

        public CourseRepository(IDbSession session)
        {
            _session = session;
        }

        public Task<long> Create(Course entity)
        {
            return _session.Connection.ExecuteScalarAsync<long>(@"INSERT INTO courses (status, name, created_by, created_date)
                OUTPUT INSERTED.id
                VALUES (1, @Name, @CreatedBy, GETDATE())", entity, _session.Transaction);
        }

        public Task Update(Course entity)
        {
            return _session.Connection.ExecuteAsync(@"UPDATE courses SET
                 name = @Name,
                 updated_by = @UpdatedBy,
                 updated_date = GETDATE()
                WHERE
                    id = @Id AND status = 1",
                entity, _session.Transaction);
        }

        public Task Delete(Course entity)
        {
            return _session.Connection.ExecuteAsync(@"UPDATE courses SET 
                 status = 2,
                 updated_by = @UpdatedBy,
                 updated_date = GETDATE()
            WHERE id = @Id AND status = 1", entity, _session.Transaction);
        }

        public Task<Course> Get(long id)
        {
            return _session.Connection.QueryFirstOrDefaultAsync<Course>(@"SELECT
                c.id as Id,
                c.status as Status,
                c.name as Name,
                c.updated_by as UpdatedBy,
                c.updated_date as UpdatedDate,
                c.created_by as CreatedBy,
                c.created_date as CreatedDate
            FROM courses c
            WHERE c.id = @Id AND c.status = 1", new { Id = id }, _session.Transaction)!;
        }

        public Task<Course> GetByName(string name)
        {
            return _session.Connection.QueryFirstOrDefaultAsync<Course>(@"SELECT
                c.id as Id,
                c.status as Status,
                c.name as Name,
                c.updated_by as UpdatedBy,
                c.updated_date as UpdatedDate,
                c.created_by as CreatedBy,
                c.created_date as CreatedDate
            FROM courses c
            WHERE TRIM(c.name) = TRIM(@Name) AND c.status = 1", new { Name = name }, _session.Transaction)!;
        }

        public async Task<FilterResponseDto<Course>> GetFilteredList(FilterRequestDto filter)
        {
            var query = @"
                SELECT
                    c.id as Id,
                    c.status as Status,
                    c.name as Name,
                    c.updated_by as UpdatedBy,
                    c.updated_date as UpdatedDate,
                    c.created_by as CreatedBy,
                    c.created_date as CreatedDate
                FROM courses c
                WHERE 
                    c.status = 1 AND
                    (@Search IS NULL OR c.name LIKE @Search)
                ORDER BY c.name
                OFFSET @Skip ROWS FETCH NEXT @Size ROWS ONLY;

                SELECT COUNT(*) 
                FROM courses c
                WHERE 
                    c.status = 1 AND
                    (@Search IS NULL OR c.name LIKE @Search);";

            using var multi = await _session.Connection
                .QueryMultipleAsync(query, new { Search = filter.SearchPattern, filter.Skip, Size = filter.CurrentPageSize }, _session.Transaction);
            var data = multi.Read<Course>().ToList();
            var totalCount = multi.ReadFirst<int>();

            return FilterResponseDto<Course>.Create(data, totalCount, filter);
        }
    }
}
