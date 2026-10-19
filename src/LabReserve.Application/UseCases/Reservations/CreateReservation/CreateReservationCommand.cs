using Mediator;

namespace LabReserve.Application.UseCases.Reservations.CreateReservation;

public record CreateReservationCommand(long RoomId, long GroupId, long UserId, DateTime Start, DateTime End)
    : ICommand, IReservationCommand
{
    public long? ReservationId => null;
}
