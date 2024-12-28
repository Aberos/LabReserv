using LabReserve.Domain.Dto;
using LabReserve.Domain.Entities;
using MediatR;

namespace LabReserve.Application.UseCases.Rooms.GetRooms;

public class GetRoomsQuery : FilterRequestDto, IRequest<FilterResponseDto<Room>>
{

}
