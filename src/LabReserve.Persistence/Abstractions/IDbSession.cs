using System.Data;

namespace LabReserve.Persistence.Abstractions;

public interface IDbSession : IDisposable
{
    IDbConnection Connection { get; }

    IDbTransaction Transaction { get; set; }
}