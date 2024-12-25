using PsttTask.Domain.Contracts;

public abstract class Entity : IEntity
{
    protected List<IDomainEventNotification> _domainEvents = new();
    public Guid Reference { get;  set; }
    public IReadOnlyCollection<IDomainEventNotification> DomainEvents => _domainEvents.AsReadOnly();

    protected void AddDomainEvent(IDomainEventNotification eventItem)
    {
        _domainEvents.Add(eventItem);
    }

    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }
}


public abstract class Entity<T> : Entity, IEntity<T>, IEntity
{
    public T Id { get; set; }
 
    public Entity()
    {
        Reference = Guid.NewGuid();
    }

    public override bool Equals(object obj)
    {
        Entity<T> entity = obj as Entity<T>;
        if ((object)entity == null)
        {
            return false;
        }

        if ((object)this == entity)
        {
            return true;
        }

        if (GetType() != entity.GetType())
        {
            return false;
        }

        if (Id.Equals(default(T)) || entity.Id.Equals(default(T)))
        {
            return false;
        }

        return Id.Equals(entity.Id);
    }

    public static bool operator ==(Entity<T> a, Entity<T> b)
    {
        if ((object)a == null && (object)b == null)
        {
            return true;
        }

        if ((object)a == null || (object)b == null)
        {
            return false;
        }

        return a.Equals(b);
    }

    public static bool operator !=(Entity<T> a, Entity<T> b)
    {
        return !(a == b);
    }

    public override int GetHashCode()
    {
        return (GetType().ToString() + Id).GetHashCode();
    }
}