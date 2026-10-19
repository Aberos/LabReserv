using LabReserve.Domain.Entities;
using Mediator;

namespace LabReserve.Application.UseCases.Rooms.GetRoom;

public record GetRoomQuery(long RoomId) : IQuery<Room>
{

}
