using LabReserve.Domain.Dto;
using LabReserve.Domain.Entities;

namespace LabReserve.Domain.Abstractions;

public interface IBaseService<T> where T : BaseEntity
{
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<T> Get(long id);

    Task<IEnumerable<T>> GetAll(FilterRequestDto filter);
}