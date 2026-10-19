using LabReserve.Domain.Abstractions;
using LabReserve.Domain.Entities;
using LabReserve.Domain.Enums;
using LabReserve.Domain.Helpers;
using Mediator;

namespace LabReserve.Application.UseCases.Users.CreateUser;

public class CreateUserHandler(IUserRepository userRepository, IAuthService authService)
    : ICommandHandler<CreateUserCommand>
{
    public async ValueTask<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = authService.Id!.Value;
        var userId = await userRepository.Create(new User
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Password = UserHelper.EncryptPassword(request.Password),
            Status = Status.Enable,
            UserType = request.UserType,
            Phone = request.Phone,
            CreatedBy = currentUserId
        });

        foreach (var groupId in request.Groups ?? [])
        {
            await userRepository.AddGroup(new GroupUser
            {
                IdGroup = groupId,
                IdUser = userId,
                Status = Status.Enable,
                CreatedBy = currentUserId
            });
        }

        return Unit.Value;
    }
}
