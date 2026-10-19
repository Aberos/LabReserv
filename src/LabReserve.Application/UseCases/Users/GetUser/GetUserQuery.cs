using LabReserve.Domain.Entities;
using Mediator;

namespace LabReserve.Application.UseCases.Users.GetUser
{
    public record GetUserQuery(long Id) : IQuery<User>
    {
    }
}
