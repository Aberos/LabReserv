using LabReserve.Domain.Abstractions;
using LabReserve.Domain.Entities;
using LabReserve.Persistence.Abstractions;

namespace LabReserve.Persistence.Repositories;

public class RoomRepository(IDbSession session) : NamedRepository<Room>(session), IRoomRepository
{
    protected override string Table => "rooms";

    public Task<long> Create(Room entity)
    {
        return Insert("name", "@Name", entity);
    }

    public Task Update(Room entity)
    {
        return UpdateColumns("name = @Name", entity);
    }
}
