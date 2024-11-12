using LabReserve.Domain.Dto;
using LabReserve.Domain.Entities;

namespace LabReserve.Domain.Abstractions;

public interface IUserService : IBaseService<User>
{
    Task<UserAuthDto> SignIn(string email, string password);
    Task SignOut();
}