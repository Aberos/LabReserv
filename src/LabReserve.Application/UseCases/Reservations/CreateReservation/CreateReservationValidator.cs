using LabReserve.Domain.Abstractions;

namespace LabReserve.Application.UseCases.Reservations.CreateReservation;

public class CreateReservationValidator(
    IRoomRepository roomRepository,
    IGroupRepository groupRepository,
    IUserRepository userRepository,
    IReservationRepository reservationRepository)
    : ReservationCommandValidator<CreateReservationCommand>(roomRepository, groupRepository, userRepository, reservationRepository);
