using FluentValidation;
using FluentValidation.Results;
using LabReserve.Domain.Abstractions;

namespace LabReserve.Application.UseCases.Rooms.CreateRoom;

public class CreateRoomValidator : AbstractValidator<CreateRoomCommand>
{
    public CreateRoomValidator(IRoomRepository roomRepository)
    {
        RuleFor(request => request.Name).NotNull()
            .CustomAsync(async (name, context, cancellationToken) =>
            {
                if (!string.IsNullOrWhiteSpace(name))
                {
                    var roomName = await roomRepository.GetByName(name);
                    if (roomName is not null)
                        context.AddFailure(new ValidationFailure("name", "already registered"));
                }
            });
    }
}
