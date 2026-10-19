using LabReserve.Domain.Abstractions;
using LabReserve.Domain.Dto;
using Mediator;

namespace LabReserve.Application.UseCases.Rooms.GetRooms;

public class GetRoomsHandler(IRoomRepository roomRepository) : IQueryHandler<GetRoomsQuery, FilterResponseDto<RoomResponse>>
{
    public async ValueTask<FilterResponseDto<RoomResponse>> Handle(GetRoomsQuery request, CancellationToken cancellationToken)
    {
        var page = await roomRepository.GetFilteredList(request);
        return page.Map(RoomResponse.From);
    }
}
