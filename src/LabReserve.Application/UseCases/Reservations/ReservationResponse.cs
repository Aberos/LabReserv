using LabReserve.Domain.Views;

namespace LabReserve.Application.UseCases.Reservations;

public record ReservationResponse(
    long Id,
    DateTime Start,
    DateTime End,
    long RoomId,
    string RoomName,
    long GroupId,
    string GroupName,
    string CourseName,
    long UserId,
    string UserName)
{
    public static ReservationResponse From(ReservationView view) => new(
        view.IdReserve,
        view.PeriodStart,
        view.PeriodEnd,
        view.IdRoom,
        view.NameRoom,
        view.IdGroup,
        view.NameGroup,
        view.NameCourse,
        view.IdUser,
        $"{view.FirstNameUser} {view.LastNameUser}".Trim());
}
