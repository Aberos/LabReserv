using LabReserve.Domain.Abstractions;
using LabReserve.Domain.Dto;
using LabReserve.Domain.Entities;
using Mediator;

namespace LabReserve.Application.UseCases.Users.GetUsers
{
    public class GetUsersHandler : IQueryHandler<GetUsersQuery, FilterResponseDto<User>>
    {
        private readonly IUserRepository _userReposistory;
        public GetUsersHandler(IUserRepository userReposistory)
        {
            _userReposistory = userReposistory;
        }


        public async ValueTask<FilterResponseDto<User>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            return await _userReposistory.GetFilteredList(request);
        }
    }
}
