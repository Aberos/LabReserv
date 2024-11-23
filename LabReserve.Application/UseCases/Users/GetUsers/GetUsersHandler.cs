using LabReserve.Domain.Abstractions;
using LabReserve.Domain.Entities;
using MediatR;

namespace LabReserve.Application.UseCases.Users.GetUsers
{
    public class GetUsersHandler : IRequestHandler<GetUsersQuery, IEnumerable<User>>
    {
        private readonly IUserRepository _userReposistory;
        public GetUsersHandler(IUserRepository userReposistory)
        {
            _userReposistory = userReposistory;
        }


        public Task<IEnumerable<User>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            return _userReposistory.GetAll(request);
        }
    }
}
