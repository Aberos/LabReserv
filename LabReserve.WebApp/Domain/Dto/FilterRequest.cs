namespace LabReserve.WebApp.Domain.Dto;

public class FilterRequest
{
    public int Size { get; set; }
    
    public int Skip { get; set; }
    
    public string? Search { get; set; }
}