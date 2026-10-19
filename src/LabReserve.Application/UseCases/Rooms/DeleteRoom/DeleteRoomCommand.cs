using MediatR;

namespace LabReserve.Application.UseCases.Rooms.DeleteRoom;

public record DeleteRoomCommand(long RoomId) : IRequest
{

}
