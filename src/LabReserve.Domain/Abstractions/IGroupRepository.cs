using LabReserve.Domain.Entities;

namespace LabReserve.Domain.Abstractions;

public interface IGroupRepository : INamedRepository<Group>
{
    Task<IEnumerable<Group>> GetByList(List<long> ids);
}
