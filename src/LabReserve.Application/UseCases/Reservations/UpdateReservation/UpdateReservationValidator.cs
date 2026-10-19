using LabReserve.Domain.Abstractions;

namespace LabReserve.Application.UseCases.Reservations.UpdateReservation;

public class UpdateReservationValidator(
    IRoomRepository roomRepository,
    IGroupRepository groupRepository,
    IUserRepository userRepository,
    IReservationRepository reservationRepository)
    : ReservationCommandValidator<UpdateReservationCommand>(roomRepository, groupRepository, userRepository, reservationRepository);
