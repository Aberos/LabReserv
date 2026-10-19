using LabReserve.Domain.Entities;
using LabReserve.Domain.Views;

namespace LabReserve.Domain.Abstractions;

public interface IReservationRepository : IBaseRepository<Reservation>
{
    Task<IEnumerable<ReservationView>> GetByPeriod(DateTime start, DateTime end, long? roomId);
    Task<ReservationView?> GetDetails(long id);
    Task<bool> HasConflict(long roomId, DateTime start, DateTime end, long? ignoredReservationId);
}
