using LabReserve.Domain.Dto;
using Mediator;

namespace LabReserve.Application.UseCases.Rooms.GetRooms;

public class GetRoomsQuery : FilterRequestDto, IQuery<FilterResponseDto<RoomResponse>>;
