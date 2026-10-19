using Dapper;
using LabReserve.Domain.Abstractions;
using LabReserve.Domain.Entities;
using LabReserve.Domain.Views;
using LabReserve.Persistence.Abstractions;

namespace LabReserve.Persistence.Repositories;

public class ReservationRepository(IDbSession session) : BaseRepository<Reservation>(session), IReservationRepository
{
    private const string SelectDetails = """
        SELECT idReserve, periodStart, periodEnd, idRoom, nameRoom, idUser, firstNameUser, lastNameUser,
               idGroup, nameGroup, nameCourse
        FROM v_reserves
        """;

    protected override string Table => "reservations";

    protected override string Columns =>
        $"{Alias}.period_start, {Alias}.period_end, {Alias}.id_room, {Alias}.id_user, {Alias}.id_group";

    protected override string SearchCondition => $"CONVERT(varchar(10), {Alias}.period_start, 23) LIKE @Search";

    protected override string OrderBy => $"{Alias}.period_start";

    public Task<long> Create(Reservation entity)
    {
        return Insert(
            "period_start, period_end, id_room, id_user, id_group",
            "@PeriodStart, @PeriodEnd, @IdRoom, @IdUser, @IdGroup",
            entity);
    }

    public Task Update(Reservation entity)
    {
        return UpdateColumns(
            "period_start = @PeriodStart, period_end = @PeriodEnd, id_room = @IdRoom, id_user = @IdUser, id_group = @IdGroup",
            entity);
    }

    public Task<IEnumerable<ReservationView>> GetByPeriod(DateTime start, DateTime end, long? roomId)
    {
        return Session.Connection.QueryAsync<ReservationView>($"""
            {SelectDetails}
            WHERE statusReserve = @Active
              AND periodStart < @End
              AND periodEnd > @Start
              AND (@RoomId IS NULL OR idRoom = @RoomId)
            ORDER BY periodStart
            """, WithStatusParameters(new { Start = start, End = end, RoomId = roomId }), Session.Transaction);
    }

    public Task<ReservationView?> GetDetails(long id)
    {
        return Session.Connection.QueryFirstOrDefaultAsync<ReservationView>(
            $"{SelectDetails} WHERE statusReserve = @Active AND idReserve = @Id",
            WithStatusParameters(new { Id = id }), Session.Transaction);
    }

    public Task<bool> HasConflict(long roomId, DateTime start, DateTime end, long? ignoredReservationId)
    {
        return Session.Connection.ExecuteScalarAsync<bool>("""
            SELECT CASE WHEN EXISTS (
                SELECT 1
                FROM reservations
                WHERE status = @Active
                  AND id_room = @RoomId
                  AND period_start < @End
                  AND period_end > @Start
                  AND (@IgnoredId IS NULL OR id <> @IgnoredId)
            ) THEN 1 ELSE 0 END
            """, WithStatusParameters(new { RoomId = roomId, Start = start, End = end, IgnoredId = ignoredReservationId }),
            Session.Transaction);
    }
}
