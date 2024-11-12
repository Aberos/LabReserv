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
        throw new NotImplementedException();
    }

    public void Update(Room entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(Room entity)
    {
        throw new NotImplementedException();
    }

    public Task<Room> Get(long id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Room>> GetAll(FilterRequestDto filter)
    {
        throw new NotImplementedException();
    }
}