using LabReserve.Domain.Dto;
using LabReserve.Domain.Entities;
using MediatR;

namespace LabReserve.Application.UseCases.Users.GetUsers
{
    public class GetUsersQuery : FilterRequestDto, IRequest<IEnumerable<User>>
    {
    }
}
