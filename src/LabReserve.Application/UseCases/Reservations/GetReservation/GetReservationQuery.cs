using Mediator;

namespace LabReserve.Application.UseCases.Reservations.GetReservation;

public record GetReservationQuery(long ReservationId) : IQuery<ReservationResponse?>;
