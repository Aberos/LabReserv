using MediatR;

namespace LabReserve.Application.UseCases.Rooms.CreateRoom;

public record CreateRoomCommand(string Name) : IRequest
{

}
