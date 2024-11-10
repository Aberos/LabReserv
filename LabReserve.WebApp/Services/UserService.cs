using LabReserve.WebApp.Domain.Abstractions;
using LabReserve.WebApp.Domain.Dto;
using LabReserve.WebApp.Domain.Entities;
using LabReserve.WebApp.Helpers;

namespace LabReserve.WebApp.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthUser _authUser;
    public UserService(IUserRepository userRepository, IAuthUser authUser)
    {
        _userRepository = userRepository;
        _authUser = authUser;
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

    public Task<IEnumerable<User>> GetAll(FilterRequestDto filter)
    {
        return _userRepository.GetAll(filter);
    }

    public async Task<UserAuthDto> SignIn(string email, string password)
    {
        var user = await _userRepository.GetByEmail(email) ?? throw new Exception("User not found");
        if(user.Password != UserHelper.EncryptPassword(password))
            throw new Exception("Invalid email or password");

        var authUser = new UserAuthDto
        {
            Id = user.Id,
            Email = user.Email,
            Name = user.GetFullName(),
            UserType = user.UserType
        };

        await _authUser.SetUser(authUser);

        return authUser;
    }

    public async Task SignOut()
    {
        await _authUser.RemoveUser();
    }
}