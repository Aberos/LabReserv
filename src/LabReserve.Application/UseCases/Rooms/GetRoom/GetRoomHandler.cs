using LabReserve.Domain.Abstractions;
using LabReserve.Domain.Entities;
using Mediator;

namespace LabReserve.Application.UseCases.Rooms.GetRoom;

public class GetRoomHandler : IQueryHandler<GetRoomQuery, Room?>
{
    private readonly IRoomRepository _roomRepository;
    public GetRoomHandler(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public async ValueTask<Room?> Handle(GetRoomQuery request, CancellationToken cancellationToken)
    {
        return await _roomRepository.Get(request.RoomId);
    }
}
