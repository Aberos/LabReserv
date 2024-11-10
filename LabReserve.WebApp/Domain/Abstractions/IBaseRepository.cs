using LabReserve.WebApp.Domain.Dto;
using LabReserve.WebApp.Domain.Entities;

namespace LabReserve.WebApp.Domain.Abstractions;

public interface IBaseRepository<T> where T : BaseEntity
{
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<T> Get(long id);
    Task<IEnumerable<T>> GetAll(FilterRequestDto filter);
}