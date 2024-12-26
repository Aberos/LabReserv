using LabReserve.Domain.Abstractions;
using LabReserve.Domain.Entities;
using MediatR;

namespace LabReserve.Application.UseCases.Rooms.GetRooms;

public class GetRoomsHandler : IRequestHandler<GetRoomsQuery, IEnumerable<Room>>
{
    private readonly IRoomRepository _roomRepository;
    public GetRoomsHandler(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public Task<IEnumerable<Room>> Handle(GetRoomsQuery request, CancellationToken cancellationToken)
    {
        return _roomRepository.GetAll(request);
    }
}
