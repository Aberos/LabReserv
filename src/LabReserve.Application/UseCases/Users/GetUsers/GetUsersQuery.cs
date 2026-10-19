using LabReserve.Domain.Dto;
using Mediator;

namespace LabReserve.Application.UseCases.Users.GetUsers;

public class GetUsersQuery : FilterRequestDto, IQuery<FilterResponseDto<UserResponse>>;
