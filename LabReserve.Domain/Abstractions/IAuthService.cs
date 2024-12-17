using LabReserve.Domain.Dto;
using LabReserve.Domain.Enums;

namespace LabReserve.Domain.Abstractions;

public interface IAuthService
{
    long? Id { get; }

    UserType UserType { get; }

    string Name { get; }

    string Email { get; }

    Task SetUser(UserAuthDto userAuthDto);

    Task RemoveUser();
}
