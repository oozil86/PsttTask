namespace PsttTask.Domain.Contracts;

public interface IUnitOfWork
{
    int Save();
    Task<int> SaveAsync(CancellationToken cancellationToken);

    void BeginTransaction();

    void Commit();

    void RollBack();
}
