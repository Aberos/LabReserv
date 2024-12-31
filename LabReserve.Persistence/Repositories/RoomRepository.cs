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

    public Task<long> Create(Room entity)
    {
        return _session.Connection.ExecuteScalarAsync<long>(@"INSERT INTO rooms (status, name, created_by, created_date)
        OUTPUT INSERTED.id
        VALUES (1, @Name, @CreatedBy, GETDATE())", entity, _session.Transaction);
    }

    public Task Update(Room entity)
    {
        return _session.Connection.ExecuteAsync(@"UPDATE rooms SET
                 email = @Name,
                 updated_by = @UpdatedBy,
                 updated_date = GETDATE()
                WHERE
                    id = @Id AND status = 1",
               entity, _session.Transaction);
    }

    public Task Delete(Room entity)
    {
        return _session.Connection.ExecuteAsync(@"UPDATE rooms SET 
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

    public Task<Room> GetByName(string name)
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
            WHERE TRIM(r.name) = TRIM(@Name) AND r.status = 1", new { Name = name }, _session.Transaction)!;
    }

    public async Task<FilterResponseDto<Room>> GetFilteredList(FilterRequestDto filter)
    {
        var search = string.IsNullOrWhiteSpace(filter?.Search) ? null : $"%{filter.Search}%";
        var requestPage = filter?.Page ?? 1;
        var size = filter?.PageSize ?? 10;
        var skip = (requestPage - 1) * size;
        var requestId = filter?.RequestId ?? 1;

        var query = @"
                    SELECT
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
                        (@Search IS NULL OR r.name LIKE @Search)
                    ORDER BY r.name
                    OFFSET @Skip ROWS FETCH NEXT @Size ROWS ONLY;

                    SELECT COUNT(*) 
                    FROM rooms r
                    WHERE 
                        r.status = 1 AND
                        (@Search IS NULL OR r.name LIKE @Search);";

        using var multi = await _session.Connection
            .QueryMultipleAsync(query, new { Search = search, Skip = skip, Size = size }, _session.Transaction);

        var data = multi.Read<Room>().ToList();
        var totalCount = multi.ReadFirst<int>();

        var totalPages = (int)Math.Ceiling(totalCount / (double)size);
        var currentPage = (skip / size) + 1;

        return new FilterResponseDto<Room>
        {
            Data = data,
            TotalCount = totalCount,
            Page = currentPage,
            TotalPages = totalPages,
            RequestId = requestId
        };
    }
}