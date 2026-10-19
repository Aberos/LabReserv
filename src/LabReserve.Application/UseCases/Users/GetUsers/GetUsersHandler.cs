using LabReserve.Domain.Abstractions;
using LabReserve.Domain.Dto;
using Mediator;

namespace LabReserve.Application.UseCases.Users.GetUsers;

public class GetUsersHandler(IUserRepository userRepository) : IQueryHandler<GetUsersQuery, FilterResponseDto<UserResponse>>
{
    public async ValueTask<FilterResponseDto<UserResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var page = await userRepository.GetFilteredList(request);
        return page.Map(UserResponse.From);
    }
}
