using Mediator;

namespace LabReserve.Application.UseCases.Reservations.DeleteReservation;

public record DeleteReservationCommand(long ReservationId) : ICommand;
