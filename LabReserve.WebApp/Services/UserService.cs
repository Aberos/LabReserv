using LabReserve.WebApp.Domain.Abstractions;
using LabReserve.WebApp.Domain.Dto;
using LabReserve.WebApp.Domain.Entities;
using LabReserve.WebApp.Helpers;

namespace LabReserve.WebApp.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;   
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public void Create(User entity)
    {
        _userRepository.Create(entity);
    }

    public void Update(User entity)
    {
        _userRepository.Update(entity);
    }

    public void Delete(User entity)
    {
        _userRepository.Delete(entity);
    }

    public Task<User> Get(long id)
    {
        return _userRepository.Get(id);
    }

    public Task<IEnumerable<User>> GetAll(FilterRequest filter)
    {
        return _userRepository.GetAll(filter);
    }

    public async Task<UserAuthDto> SignIn(string email, string password)
    {
        var user = await _userRepository.GetByEmail(email) ?? throw new Exception("User not found");
        if(user.Password != UserHelper.EncryptPassword(password))
            throw new Exception("Invalid email or password");
        
        return new UserAuthDto
        {
            Id = user.Id,
            Email = user.Email,
            Name = user.GetFullName(),
            UserType = user.UserType
        };
    }
}