using LabReserve.WebApp.Domain.Enums;

namespace LabReserve.WebApp.Domain.Dto
{
    public record AuthResponse(string Email , UserType Type, string Name)
    {
    }
}
