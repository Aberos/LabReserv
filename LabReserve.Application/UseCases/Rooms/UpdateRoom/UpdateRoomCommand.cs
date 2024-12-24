using MediatR;

namespace LabReserve.Application.UseCases.Rooms.UpdateRoom;

public record UpdateRoomCommand(string Name, long RoomId) : IRequest
{

}
