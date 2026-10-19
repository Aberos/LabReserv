namespace LabReserve.Domain.Dto;

public class FilterRequestDto
{
    private const int FirstPage = 1;
    private const int DefaultPageSize = 10;

    public int Page { get; set; }

    public int PageSize { get; set; }

    public string? Search { get; set; }

    public int RequestId { get; set; }

    public int CurrentPage => Page >= FirstPage ? Page : FirstPage;

    public int CurrentPageSize => PageSize > 0 ? PageSize : DefaultPageSize;

    public int Skip => (CurrentPage - FirstPage) * CurrentPageSize;

    public string? SearchPattern => string.IsNullOrWhiteSpace(Search) ? null : $"%{Search.Trim()}%";
}
