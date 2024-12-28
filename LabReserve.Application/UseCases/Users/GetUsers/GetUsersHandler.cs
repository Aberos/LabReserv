using LabReserve.Domain.Abstractions;
using LabReserve.Domain.Dto;
using LabReserve.Domain.Entities;
using MediatR;

namespace LabReserve.Application.UseCases.Users.GetUsers
{
    public class GetUsersHandler : IRequestHandler<GetUsersQuery, FilterResponseDto<User>>
    {
        private readonly IUserRepository _userReposistory;
        public GetUsersHandler(IUserRepository userReposistory)
        {
            _userReposistory = userReposistory;
        }


        public Task<FilterResponseDto<User>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            return _userReposistory.GetFilteredList(request);
        }
    }
}
