using LabReserve.Domain.Entities;

namespace LabReserve.Application.UseCases.Rooms;

public record RoomResponse(long Id, string Name)
{
    public static RoomResponse From(Room room) => new(room.Id, room.Name);
}
