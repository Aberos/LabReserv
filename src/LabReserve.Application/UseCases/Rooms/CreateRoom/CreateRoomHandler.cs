using LabReserve.Domain.Abstractions;
using LabReserve.Domain.Entities;
using Mediator;

namespace LabReserve.Application.UseCases.Rooms.CreateRoom;

public class CreateRoomHandler(IRoomRepository roomRepository, IAuthService authService)
    : ICommandHandler<CreateRoomCommand>
{
    public async ValueTask<Unit> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
    {
        await roomRepository.Create(new Room
        {
            Name = request.Name,
            CreatedBy = authService.Id!.Value
        });

        return Unit.Value;
    }
}
