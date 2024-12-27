using MediatR;

namespace LabReserve.Application.UseCases.Rooms.UpdateRoom;

public class UpdateRoomCommand : IRequest
{
    public long RoomId { get; set; }
    public string Name { get; set; }
}
