using System.Data;
using LabReserve.Domain.Abstractions;
using LabReserve.Persistence.Abstractions;

namespace LabReserve.Persistence;

public class UnitOfWork(IDbSession session) : IUnitOfWork
{
    public void BeginTransaction()
    {
        session.Transaction = session.Connection.BeginTransaction();
    }

    public void Commit()
    {
        ActiveTransaction.Commit();
        Dispose();
    }

    public void Rollback()
    {
        ActiveTransaction.Rollback();
        Dispose();
    }

    public void Dispose()
    {
        session.Transaction?.Dispose();
        session.Transaction = null;
    }

    private IDbTransaction ActiveTransaction =>
        session.Transaction ?? throw new InvalidOperationException("No active transaction.");
}
