using FluentValidation;
using FluentValidation.Results;
using LabReserve.Domain.Abstractions;

namespace LabReserve.Application.UseCases.Rooms.UpdateRoom;

public class UpdateRoomValidator : AbstractValidator<UpdateRoomCommand>
{
    public UpdateRoomValidator(IRoomRepository roomRepository)
    {
        RuleFor(request => request.Name).NotNull()
            .CustomAsync(async (name, context, cancellationToken) =>
            {
                if (!string.IsNullOrWhiteSpace(name))
                {
                    var roomName = await roomRepository.GetByName(name);
                    if (roomName is not null && roomName.Id != context.InstanceToValidate.RoomId)
                        context.AddFailure(new ValidationFailure("name", "already registered"));
                }
            });
    }
}
