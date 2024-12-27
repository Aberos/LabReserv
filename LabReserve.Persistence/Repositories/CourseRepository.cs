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
                VALUES (1, @Name, @CreatedBy, GETDATE()) RETURNING id", entity, _session.Transaction);
        }

        public Task Update(Course entity)
        {
            return _session.Connection.ExecuteAsync(@"UPDATE courses SET
                 email = @Name,
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

        public Task<IEnumerable<Course>> GetAll(FilterRequestDto filter)
        {
            return _session.Connection.QueryAsync<Course>(@"SELECT
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
                (@Search is null OR c.name like @Search)",
            new
            {
                Search = string.IsNullOrWhiteSpace(filter?.Search) ? $"%{filter.Search}%" : null,
                Skip = filter?.Skip ?? 0,
                Size = filter?.Size ?? 1,
            }, _session.Transaction);
        }
    }
}
