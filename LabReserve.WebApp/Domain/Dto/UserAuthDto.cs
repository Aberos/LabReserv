using LabReserve.WebApp.Domain.Enums;

namespace LabReserve.WebApp.Domain.Dto;

public class UserAuthDto
{
    public long Id { get; set; }
    
    public string Name { get; set; }
    
    public string Email { get; set; }
    
    public UserType UserType { get; set; }
}