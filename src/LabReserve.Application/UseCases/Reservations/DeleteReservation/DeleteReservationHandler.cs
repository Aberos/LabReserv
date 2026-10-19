using LabReserve.Domain.Abstractions;
using Mediator;

namespace LabReserve.Application.UseCases.Reservations.DeleteReservation;

public class DeleteReservationHandler(IReservationRepository reservationRepository, IAuthService authService)
    : ICommandHandler<DeleteReservationCommand>
{
    public async ValueTask<Unit> Handle(DeleteReservationCommand request, CancellationToken cancellationToken)
    {
        var reservation = await reservationRepository.Get(request.ReservationId)
            ?? throw new KeyNotFoundException("Reservation not found");

        reservation.UpdatedBy = authService.Id!.Value;
        await reservationRepository.Delete(reservation);

        return Unit.Value;
    }
}
