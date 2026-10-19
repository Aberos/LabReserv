using LabReserve.Domain.Entities;

namespace LabReserve.Domain.Abstractions;

public interface INamedRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    Task<T?> GetByName(string name);
}
