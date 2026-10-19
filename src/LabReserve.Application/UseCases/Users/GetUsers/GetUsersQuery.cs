using LabReserve.Domain.Dto;
using LabReserve.Domain.Entities;
using Mediator;

namespace LabReserve.Application.UseCases.Users.GetUsers
{
    public class GetUsersQuery : FilterRequestDto, IQuery<FilterResponseDto<User>>
    {
    }
}
