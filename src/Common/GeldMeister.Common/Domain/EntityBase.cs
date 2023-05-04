using System.Text.Json.Serialization;

namespace GeldMeister.Common.Domain;

public interface IEntity<T>
{
    public T Id { get; set; }
}

public class EntityBase : IEntity<Guid>
{
    public Guid Id { get; set; }

    private readonly List<DomainEvent> _domainEvents = new();

    [JsonIgnore]
    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void Raise(DomainEvent @event)
    {
        _domainEvents.Add(@event);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}