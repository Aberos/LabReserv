using LabReserve.WebApp.Domain.Enums;

namespace LabReserve.WebApp.Domain.Entities;

public class User : BaseEntity
{
    public string Email { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string Phone { get; set; }
    
    public string Password { get; set; }
    
    public UserType UserType { get; set; }

    public string GetFullName()
    {
        var name = FirstName;
        if(!string.IsNullOrWhiteSpace(LastName))
            name += $" {LastName}";
        
        return name;
    }
}