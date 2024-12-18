﻿using Dapper;
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
            return _session.Connection.ExecuteScalarAsync<long>(@"INSERT INTO groups (status, name, idCourse, created_by, created_date)
                VALUES (1, @Name, @IdCourse, @CreatedBy, GETDATE()) RETURNING id", entity, _session.Transaction);
        }
        public Task Update(Group entity)
        {
            return _session.Connection.ExecuteAsync(@"UPDATE groups SET
                 email = @Name,
                 idCourse = @IdCourse,
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

        public Task<Group> Get(long id)
        {
            return _session.Connection.QueryFirstOrDefaultAsync<Group>(@"SELECT
                g.id as Id,
                g.status as Status,
                g.name as Name,
                g.idCourse as IdCourse,
                g.updated_by as UpdatedBy,
                g.updated_date as UpdatedDate,
                g.created_by as CreatedBy,
                g.created_date as CreatedDate
            FROM groups g
            WHERE g.id = @Id AND g.status = 1", new { Id = id }, _session.Transaction)!;
        }

        public Task<IEnumerable<Group>> GetAll(FilterRequestDto filter)
        {
            return _session.Connection.QueryAsync<Group>(@"SELECT
                g.id as Id,
                g.status as Status,
                g.name as Name,
                g.idCourse as IdCourse,
                g.updated_by as UpdatedBy,
                g.updated_date as UpdatedDate,
                g.created_by as CreatedBy,
                g.created_date as CreatedDate
            FROM groups g
            WHERE 
                g.status = 1 AND
                (@Search is null OR g.name like @Search)",
            new
            {
                Search = !string.IsNullOrWhiteSpace(filter?.Search) ? $"%{filter.Search}%" : null,
                Skip = filter?.Skip ?? 0,
                Size = filter?.Size ?? 1,
            }, _session.Transaction);
        }

        public Task<IEnumerable<Group>> GetByList(List<long> ids)
        {
            return _session.Connection.QueryAsync<Group>(@"SELECT
                g.id as Id,
                g.status as Status,
                g.name as Name,
                g.idCourse as IdCourse,
                g.updated_by as UpdatedBy,
                g.updated_date as UpdatedDate,
                g.created_by as CreatedBy,
                g.created_date as CreatedDate
            FROM groups g
            WHERE g.id in (@Ids) AND g.status = 1", new { Ids = ids }, _session.Transaction)!;
        }
    }
}
