namespace LabReserve.Domain.Dto;

public class FilterRequestDto
{
    public int Page { get; set; }

    public int PageSize { get; set; }

    public string? Search { get; set; }

    public int RequestId { get; set; }
}