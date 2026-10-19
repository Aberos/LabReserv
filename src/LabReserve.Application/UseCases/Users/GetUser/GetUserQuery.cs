using LabReserve.Domain.Entities;
using MediatR;

namespace LabReserve.Application.UseCases.Users.GetUser
{
    public record GetUserQuery(long Id) : IRequest<User>
    {
    }
}
