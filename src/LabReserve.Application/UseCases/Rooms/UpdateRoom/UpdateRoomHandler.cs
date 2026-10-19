using LabReserve.Domain.Abstractions;
using Mediator;

namespace LabReserve.Application.UseCases.Rooms.UpdateRoom;

public class UpdateRoomHandler(IRoomRepository roomRepository, IAuthService authService)
    : ICommandHandler<UpdateRoomCommand>
{
    public async ValueTask<Unit> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
    {
        var room = await roomRepository.Get(request.RoomId) ?? throw new KeyNotFoundException("Room not found");
        room.Name = request.Name;
        room.UpdatedBy = authService.Id!.Value;

        await roomRepository.Update(room);

        return Unit.Value;
    }
}
