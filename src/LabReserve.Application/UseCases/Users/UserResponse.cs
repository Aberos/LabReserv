using LabReserve.Domain.Entities;
using LabReserve.Domain.Enums;

namespace LabReserve.Application.UseCases.Users;

public record UserResponse(long Id, string Email, string FirstName, string LastName, string Phone, UserType UserType)
{
    public static UserResponse From(User user) =>
        new(user.Id, user.Email, user.FirstName, user.LastName, user.Phone, user.UserType);
}
