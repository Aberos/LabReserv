namespace LabReserve.WebApp.Domain.Entities;

public class Request : BaseEntity
{
    public DateTime PeriodStart { get; set; }
    
    public DateTime PeriodEnd { get; set; }
    
    public long IdRoom { get; set; }
    
    public long IdUser { get; set; }
    
    public long IdGroup  { get; set; }
}