using FluentValidation;
using LabReserve.Domain.Abstractions;

namespace LabReserve.Application.UseCases.Reservations;

public abstract class ReservationCommandValidator<T> : AbstractValidator<T> where T : IReservationCommand
{
    protected ReservationCommandValidator(
        IRoomRepository roomRepository,
        IGroupRepository groupRepository,
        IUserRepository userRepository,
        IReservationRepository reservationRepository)
    {
        RuleFor(request => request.RoomId)
            .MustAsync(async (roomId, _) => await roomRepository.Get(roomId) is not null)
            .OverridePropertyName("roomId").WithMessage("room not found");

        RuleFor(request => request.GroupId)
            .MustAsync(async (groupId, _) => await groupRepository.Get(groupId) is not null)
            .OverridePropertyName("groupId").WithMessage("group not found");

        RuleFor(request => request.UserId)
            .MustAsync(async (userId, _) => await userRepository.Get(userId) is not null)
            .OverridePropertyName("userId").WithMessage("professor not found");

        RuleFor(request => request.End)
            .GreaterThan(request => request.Start)
            .OverridePropertyName("end").WithMessage("end must be after start");

        RuleFor(request => request)
            .MustAsync(async (request, _) => !await reservationRepository.HasConflict(
                request.RoomId, request.Start, request.End, request.ReservationId))
            .When(request => request.End > request.Start)
            .OverridePropertyName("period").WithMessage("room already reserved in this period");
    }
}
