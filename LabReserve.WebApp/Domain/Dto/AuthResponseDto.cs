using LabReserve.WebApp.Domain.Enums;

namespace LabReserve.WebApp.Domain.Dto
{
    public record AuthResponseDto(string Email , UserType Type, string Name)
    {
    }
}
