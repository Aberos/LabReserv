using LabReserve.Domain.Entities;
using MediatR;

namespace LabReserve.Application.UseCases.Rooms.GetRoom;

public record GetRoomQuery(long RoomId) : IRequest<Room>
{

}
