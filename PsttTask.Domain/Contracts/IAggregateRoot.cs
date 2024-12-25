namespace PsttTask.Domain.Contracts;
public interface IAggregateRoot<T> : IAggregateRoot, IEntity<T>, IEntity
{
    void SoftDelete();
}

public interface IAggregateRoot
{
    string CreatedBy { get; set; }

    DateTime CreationDate { get; set; }

    bool IsDeleted { get; set; }

    string UpdatedBy { get; set; }

    DateTime? UpdationDate { get; set; }
}