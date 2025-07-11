using System.Collections.ObjectModel;

namespace FC4.HotelReservation.Domain.Common;

public abstract class AggregateRoot : Entity
{
    private readonly HashSet<DomainEvent> _events = [];
    public IReadOnlyCollection<DomainEvent> Events => new ReadOnlyCollection<DomainEvent>(_events.ToList());
    
    protected void RaiseEvent(DomainEvent @event) => _events.Add(@event);
    public void RemoveEvent(DomainEvent @event) => _events.Remove(@event);
}