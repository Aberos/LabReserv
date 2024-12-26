using LabReserve.Domain.Abstractions;
using LabReserve.Domain.Entities;
using MediatR;

namespace LabReserve.Application.UseCases.Rooms.GetRoom;

public class GetRoomHandler : IRequestHandler<GetRoomQuery, Room>
{
    private readonly IRoomRepository _roomRepository;
    public GetRoomHandler(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public Task<Room> Handle(GetRoomQuery request, CancellationToken cancellationToken)
    {
        return _roomRepository.Get(request.RoomId);
    }
}
