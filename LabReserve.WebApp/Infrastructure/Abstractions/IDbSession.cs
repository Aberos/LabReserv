using System.Data;

namespace LabReserve.WebApp.Infrastructure.Abstractions;

public interface IDbSession: IDisposable
{
    IDbConnection Connection { get; }
    
    IDbTransaction Transaction { get; set; }
}