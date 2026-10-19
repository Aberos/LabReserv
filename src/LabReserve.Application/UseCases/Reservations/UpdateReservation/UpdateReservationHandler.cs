using LabReserve.Domain.Abstractions;
using Mediator;

namespace LabReserve.Application.UseCases.Reservations.UpdateReservation;

public class UpdateReservationHandler(IReservationRepository reservationRepository, IAuthService authService)
    : ICommandHandler<UpdateReservationCommand>
{
    public async ValueTask<Unit> Handle(UpdateReservationCommand request, CancellationToken cancellationToken)
    {
        var reservation = await reservationRepository.Get(request.ReservationId)
            ?? throw new KeyNotFoundException("Reservation not found");

        reservation.IdRoom = request.RoomId;
        reservation.IdGroup = request.GroupId;
        reservation.IdUser = request.UserId;
        reservation.PeriodStart = request.Start;
        reservation.PeriodEnd = request.End;
        reservation.UpdatedBy = authService.Id!.Value;

        await reservationRepository.Update(reservation);

        return Unit.Value;
    }
}
