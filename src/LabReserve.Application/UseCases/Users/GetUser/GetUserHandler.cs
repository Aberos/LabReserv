using LabReserve.Domain.Abstractions;
using Mediator;

namespace LabReserve.Application.UseCases.Users.GetUser;

public class GetUserHandler(IUserRepository userRepository) : IQueryHandler<GetUserQuery, UserResponse?>
{
    public async ValueTask<UserResponse?> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.Get(request.Id);
        return user is null ? null : UserResponse.From(user);
    }
}
