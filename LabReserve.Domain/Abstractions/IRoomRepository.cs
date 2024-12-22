using LabReserve.Domain.Entities;

namespace LabReserve.Domain.Abstractions;

public interface IRoomRepository : IBaseRepository<Room>
{
    Task<Room> GetByName(string name);
}