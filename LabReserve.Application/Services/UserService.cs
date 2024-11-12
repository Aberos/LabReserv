using LabReserve.Domain.Abstractions;
using LabReserve.Domain.Dto;
using LabReserve.Domain.Entities;
using LabReserve.Domain.Helpers;

namespace LabReserve.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthService _authService;
    public UserService(IUserRepository userRepository, IAuthService authService)
    {
        _userRepository = userRepository;
        _authService = authService;
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
        if (user.Password != UserHelper.EncryptPassword(password))
            throw new Exception("Invalid email or password");

        var userAuth = new UserAuthDto
        {
            Id = user.Id,
            Email = user.Email,
            Name = user.GetFullName(),
            UserType = user.UserType
        };

        await _authService.SetUser(userAuth);

        return userAuth;
    }

    public async Task SignOut()
    {
        await _authService.RemoveUser();
    }
}