using LabReserve.Domain.Entities;

namespace LabReserve.Domain.Abstractions
{
    public interface IGroupRepository : IBaseRepository<Group>
    {
        Task<IEnumerable<Group>> GetByList(List<long> ids);
    }
}
