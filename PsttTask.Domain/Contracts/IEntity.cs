namespace PsttTask.Domain.Contracts;

public interface IEntity
{
    Guid Reference { get; set; }
}

public interface IEntity<T> : IEntity
{
    T Id { get; }
}

