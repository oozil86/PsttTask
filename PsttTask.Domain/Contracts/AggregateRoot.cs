namespace PsttTask.Domain.Contracts;

public abstract class AggregateRoot<T> : Entity<T>, IAggregateRoot<T>, IEntity<T>, IEntity
{
    public string CreatedBy { get; set; }

    public DateTime CreationDate { get; set; }

    public string UpdatedBy { get; set; }

    public DateTime? UpdationDate { get; set; }

    public bool IsDeleted { get; set; }

    public void SoftDelete()
    {
        IsDeleted = true;
    }
}