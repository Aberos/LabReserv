using LabReserve.WebApp.Domain.Dto;
using LabReserve.WebApp.Domain.Entities;

namespace LabReserve.WebApp.Domain.Abstractions;

public interface IUserService : IBaseService<User>
{
   Task<UserAuthDto> SignIn(string email, string password); 
}