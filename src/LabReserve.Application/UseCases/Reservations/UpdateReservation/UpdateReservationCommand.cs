using Mediator;

namespace LabReserve.Application.UseCases.Reservations.UpdateReservation;

public class UpdateReservationCommand : ICommand, IReservationCommand
{
    public long ReservationId { get; set; }
    public long RoomId { get; set; }
    public long GroupId { get; set; }
    public long UserId { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }

    long? IReservationCommand.ReservationId => ReservationId;
}
