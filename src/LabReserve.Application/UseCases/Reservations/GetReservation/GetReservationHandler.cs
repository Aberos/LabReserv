using LabReserve.Domain.Abstractions;
using Mediator;

namespace LabReserve.Application.UseCases.Reservations.GetReservation;

public class GetReservationHandler(IReservationRepository reservationRepository)
    : IQueryHandler<GetReservationQuery, ReservationResponse?>
{
    public async ValueTask<ReservationResponse?> Handle(GetReservationQuery request, CancellationToken cancellationToken)
    {
        var reservation = await reservationRepository.GetDetails(request.ReservationId);
        return reservation is null ? null : ReservationResponse.From(reservation);
    }
}
