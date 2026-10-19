using Mediator;

namespace LabReserve.Application.UseCases.Rooms.UpdateRoom;

public class UpdateRoomCommand : ICommand
{
    public long RoomId { get; set; }
    public string Name { get; set; }
}
