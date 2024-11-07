using LabReserve.WebApp.Domain.Abstractions;
using LabReserve.WebApp.Infrastructure.Abstractions;
using LabReserve.WebApp.Infrastructure.Database;

namespace LabReserve.WebApp.Infrastructure;

public class UnitOfWork: IUnitOfWork
{
    private readonly IDbSession _session;

    public UnitOfWork(IDbSession session)
    {
        _session = session;
    }

    public void BeginTransaction()
    {
        _session.Transaction = _session.Connection.BeginTransaction();
    }

    public void Commit()
    {
        _session.Transaction.Commit();
        Dispose();
    }

    public void Rollback()
    {
        _session.Transaction.Rollback();
        Dispose();
    }

    public void Dispose() => _session.Transaction?.Dispose();
}