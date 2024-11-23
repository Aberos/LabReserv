using Dapper;
using LabReserve.Domain.Abstractions;
using LabReserve.Domain.Dto;
using LabReserve.Domain.Entities;
using LabReserve.Persistence.Abstractions;

namespace LabReserve.Persistence.Repositories;

public class RoomRepository : IRoomRepository
{
    private readonly IDbSession _session;

    public RoomRepository(IDbSession session)
    {
        _session = session;
    }

    public void Create(Room entity)
    {
        _session.Connection.Execute(@"INSERT INTO rooms (status, name, created_by, created_date)
                VALUES (1, @Name, @CreatedBy, GETDATE())", entity, _session.Transaction);
    }

    public void Update(Room entity)
    {
        _session.Connection.Execute(@"UPDATE rooms SET
                 email = @Name,
                 updated_by = @UpdatedBy,
                 updated_date = GETDATE()
                WHERE
                    id = @Id AND status = 1", 
               entity, _session.Transaction);
    }

    public void Delete(Room entity)
    {
        _session.Connection.Execute(@"UPDATE rooms SET 
                 status = 2,
                 updated_by = @UpdatedBy,
                 updated_date = GETDATE()
            WHERE id = @Id AND status = 1", entity, _session.Transaction);
    }

    public Task<Room> Get(long id)
    {
        return _session.Connection.QueryFirstOrDefaultAsync<Room>(@"SELECT
                r.id as Id,
                r.status as Status,
                r.name as Name,
                r.updated_by as UpdatedBy,
                r.updated_date as UpdatedDate,
                r.created_by as CreatedBy,
                r.created_date as CreatedDate
            FROM rooms r
            WHERE r.id = @Id AND r.status = 1", new { Id = id }, _session.Transaction)!;
    }

    public Task<IEnumerable<Room>> GetAll(FilterRequestDto filter)
    {
        return _session.Connection.QueryAsync<Room>(@"SELECT
                r.id as Id,
                r.status as Status,
                r.name as Name,
                r.updated_by as UpdatedBy,
                r.updated_date as UpdatedDate,
                r.created_by as CreatedBy,
                r.created_date as CreatedDate
            FROM rooms r
            WHERE 
                r.status = 1 AND
                (@Search is null OR r.name like @Search)",
            new
            {
                Search = string.IsNullOrWhiteSpace(filter?.Search) ? $"%{filter.Search}%" : null,
                Skip = filter?.Skip ?? 0,
                Size = filter?.Size ?? 1,
            }, _session.Transaction);
    }
}