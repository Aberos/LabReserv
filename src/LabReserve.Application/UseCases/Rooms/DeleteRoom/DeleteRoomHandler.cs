using LabReserve.Domain.Abstractions;
using Mediator;

namespace LabReserve.Application.UseCases.Rooms.DeleteRoom;

public class DeleteRoomHandler(IRoomRepository roomRepository, IAuthService authService)
    : ICommandHandler<DeleteRoomCommand>
{
    public async ValueTask<Unit> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
    {
        var room = await roomRepository.Get(request.RoomId) ?? throw new KeyNotFoundException("Room not found");
        room.UpdatedBy = authService.Id!.Value;

        await roomRepository.Delete(room);

        return Unit.Value;
    }
}
