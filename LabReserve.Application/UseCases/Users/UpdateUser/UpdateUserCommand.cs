using LabReserve.Domain.Enums;
using MediatR;

namespace LabReserve.Application.UseCases.Users.UpdateUser;

public class UpdateUserCommand : IRequest
{
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
    public UserType UserType { get; set; }
    public List<long> Groups { get; set; }
    public long UserId { get; set; }
}
