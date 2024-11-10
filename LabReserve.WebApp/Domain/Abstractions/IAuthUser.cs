using LabReserve.WebApp.Domain.Dto;
using LabReserve.WebApp.Domain.Enums;

namespace LabReserve.WebApp.Domain.Abstractions
{
    public interface IAuthUser
    {
        long? Id { get; }

        UserType UserType { get; }

        string Name { get; }

        string Email { get; }

        Task SetUser(UserAuthDto userAuthDto);

        Task RemoveUser();
    }
}
