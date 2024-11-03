namespace LabReserve.WebApp.Domain.Abstractions;

public interface IUnitOfWork: IDisposable
{
    void BeginTransaction();
    void Commit();
    void Rollback();
}