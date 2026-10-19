using LabReserve.Domain.Abstractions;
using LabReserve.Domain.Entities;
using LabReserve.Domain.Enums;
using LabReserve.Domain.Helpers;
using Mediator;

namespace LabReserve.Application.UseCases.Users.UpdateUser;

public class UpdateUserHandler(IUserRepository userRepository, IAuthService authService)
    : ICommandHandler<UpdateUserCommand>
{
    public async ValueTask<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = authService.Id!.Value;
        var user = await userRepository.Get(request.UserId) ?? throw new KeyNotFoundException("User not found");

        await userRepository.RemoveAllGroup(user.Id, currentUserId);

        user.Email = request.Email;
        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.Phone = request.Phone;
        user.UserType = request.UserType;
        user.UpdatedBy = currentUserId;

        await userRepository.Update(user);

        if (!string.IsNullOrEmpty(request.Password))
        {
            user.Password = UserHelper.EncryptPassword(request.Password);
            await userRepository.UpdatePassword(user);
        }

        foreach (var groupId in request.Groups ?? [])
        {
            await userRepository.AddGroup(new GroupUser
            {
                IdGroup = groupId,
                IdUser = user.Id,
                Status = Status.Enable,
                CreatedBy = currentUserId
            });
        }

        return Unit.Value;
    }
}
