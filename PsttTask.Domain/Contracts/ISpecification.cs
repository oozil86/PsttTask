namespace PsttTask.Domain.Contracts;

public interface ISpecification<T>
{
    T Query(CancellationToken cancellationToken = default);
}
