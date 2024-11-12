using LabReserve.Domain.Enums;

namespace LabReserve.Domain.Dto;

public class UserAuthDto
{
    public long Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public UserType UserType { get; set; }
}