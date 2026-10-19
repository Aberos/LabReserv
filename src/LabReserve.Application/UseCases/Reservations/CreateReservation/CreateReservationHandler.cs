using LabReserve.Domain.Abstractions;
using LabReserve.Domain.Entities;
using Mediator;

namespace LabReserve.Application.UseCases.Reservations.CreateReservation;

public class CreateReservationHandler(IReservationRepository reservationRepository, IAuthService authService)
    : ICommandHandler<CreateReservationCommand>
{
    public async ValueTask<Unit> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
    {
        await reservationRepository.Create(new Reservation
        {
            IdRoom = request.RoomId,
            IdGroup = request.GroupId,
            IdUser = request.UserId,
            PeriodStart = request.Start,
            PeriodEnd = request.End,
            CreatedBy = authService.Id!.Value
        });

        return Unit.Value;
    }
}
