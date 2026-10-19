namespace LabReserve.Application.UseCases.Reservations;

public interface IReservationCommand
{
    long? ReservationId { get; }
    long RoomId { get; }
    long GroupId { get; }
    long UserId { get; }
    DateTime Start { get; }
    DateTime End { get; }
}
