using LabReserve.Domain.Abstractions;
using Mediator;

namespace LabReserve.Application.UseCases.Rooms.GetRoom;

public class GetRoomHandler(IRoomRepository roomRepository) : IQueryHandler<GetRoomQuery, RoomResponse?>
{
    public async ValueTask<RoomResponse?> Handle(GetRoomQuery request, CancellationToken cancellationToken)
    {
        var room = await roomRepository.Get(request.RoomId);
        return room is null ? null : RoomResponse.From(room);
    }
}
