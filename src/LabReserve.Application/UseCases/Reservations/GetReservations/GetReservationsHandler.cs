using LabReserve.Domain.Abstractions;
using Mediator;

namespace LabReserve.Application.UseCases.Reservations.GetReservations;

public class GetReservationsHandler(IReservationRepository reservationRepository)
    : IQueryHandler<GetReservationsQuery, IEnumerable<ReservationResponse>>
{
    public async ValueTask<IEnumerable<ReservationResponse>> Handle(GetReservationsQuery request, CancellationToken cancellationToken)
    {
        var reservations = await reservationRepository.GetByPeriod(request.Start, request.End, request.RoomId);
        return reservations.Select(ReservationResponse.From).ToList();
    }
}
