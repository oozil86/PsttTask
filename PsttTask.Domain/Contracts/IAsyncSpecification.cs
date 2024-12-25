namespace PsttTask.Domain.Contract;

public interface IAsyncSpecification<T>
{
    public Task<T> Query(CancellationToken cancellationToken = default);
}
