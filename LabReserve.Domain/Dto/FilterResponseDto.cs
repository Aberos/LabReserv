using LabReserve.Domain.Entities;

namespace LabReserve.Domain.Dto;

public class FilterResponseDto<T> where T : BaseEntity
{
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public IEnumerable<T> Data { get; set; }
    public int RequestId { get; set; }
}