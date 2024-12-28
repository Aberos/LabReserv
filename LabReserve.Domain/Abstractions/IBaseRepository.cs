using LabReserve.Domain.Dto;
using LabReserve.Domain.Entities;

namespace LabReserve.Domain.Abstractions;

public interface IBaseRepository<T> where T : BaseEntity
{
    Task<long> Create(T entity);
    Task Update(T entity);
    Task Delete(T entity);
    Task<T> Get(long id);
    Task<FilterResponseDto<T>> GetFilteredList(FilterRequestDto filter);
}