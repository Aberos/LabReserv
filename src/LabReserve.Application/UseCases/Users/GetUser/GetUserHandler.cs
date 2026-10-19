using LabReserve.Domain.Abstractions;
using LabReserve.Domain.Entities;
using Mediator;

namespace LabReserve.Application.UseCases.Users.GetUser
{
    public class GetUserHandler : IQueryHandler<GetUserQuery, User>
    {
        private readonly IUserRepository _userRepository;
        public GetUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async ValueTask<User> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.Get(request.Id);
        }
    }
}
