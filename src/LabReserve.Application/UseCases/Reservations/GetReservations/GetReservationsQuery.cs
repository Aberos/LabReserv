using Mediator;

namespace LabReserve.Application.UseCases.Reservations.GetReservations;

public class GetReservationsQuery : IQuery<IEnumerable<ReservationResponse>>
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public long? RoomId { get; set; }
}
