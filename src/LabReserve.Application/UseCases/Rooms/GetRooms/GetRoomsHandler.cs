using LabReserve.Domain.Abstractions;
using LabReserve.Domain.Dto;
using LabReserve.Domain.Entities;
using Mediator;

namespace LabReserve.Application.UseCases.Rooms.GetRooms;

public class GetRoomsHandler : IQueryHandler<GetRoomsQuery, FilterResponseDto<Room>>
{
    private readonly IRoomRepository _roomRepository;
    public GetRoomsHandler(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public async ValueTask<FilterResponseDto<Room>> Handle(GetRoomsQuery request, CancellationToken cancellationToken)
    {
        return await _roomRepository.GetFilteredList(request);
    }
}
