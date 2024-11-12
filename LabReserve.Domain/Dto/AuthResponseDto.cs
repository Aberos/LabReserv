using LabReserve.Domain.Enums;

namespace LabReserve.Domain.Dto;

public record AuthResponseDto(string Email, UserType Type, string Name)
{
}
