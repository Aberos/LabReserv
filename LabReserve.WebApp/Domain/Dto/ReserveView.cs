using LabReserve.WebApp.Domain.Enums;

namespace LabReserve.WebApp.Domain.Dto;

public class ReserveView
{
    public long IdReserve { get; set; }
    
    public DateTime PeriodStart { get; set; }
    
    public DateTime PeriodEnd { get; set; }
    
    public long IdRoom { get; set; }
    
    public string NameRoom { get; set; }
    
    public Status StatusRoom { get; set; }
    
    public long IdCreator { get; set; }
    
    public string FirstNameCreator { get; set; }
    
    public string LastNameCreator { get; set; }
    
    public Status StatusCreator { get; set; }
    
    public long IdUser { get; set; }
    
    public string FirstNameUser { get; set; }
    
    public string LastNameUser { get; set; }
    
    public UserType UserType { get; set; }
    
    public Status StatusUser { get; set; }
    
    public long IdGroup { get; set; }
    
    public string NameGroup { get; set; }
    
    public Status StatusGroup { get; set; }
    
    public long IdCourse { get; set; }
    
    public string NameCourse { get; set; }
    
    public Status StatusCourse { get; set; }
}