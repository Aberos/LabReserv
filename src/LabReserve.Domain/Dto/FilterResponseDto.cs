namespace LabReserve.Domain.Dto;

public class FilterResponseDto<T> where T : class
{
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public IEnumerable<T> Data { get; set; } = [];
    public int RequestId { get; set; }

    public static FilterResponseDto<T> Create(IEnumerable<T> data, int totalCount, FilterRequestDto filter) => new()
    {
        Data = data,
        TotalCount = totalCount,
        Page = filter.CurrentPage,
        PageSize = filter.CurrentPageSize,
        TotalPages = (int)Math.Ceiling(totalCount / (double)filter.CurrentPageSize),
        RequestId = filter.RequestId
    };

    public FilterResponseDto<TResult> Map<TResult>(Func<T, TResult> map) where TResult : class => new()
    {
        Data = Data.Select(map).ToList(),
        TotalCount = TotalCount,
        Page = Page,
        PageSize = PageSize,
        TotalPages = TotalPages,
        RequestId = RequestId
    };
}
