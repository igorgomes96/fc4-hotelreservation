using System.Collections.ObjectModel;

namespace FC4.HotelReservation.Domain.Common;

public abstract class AggregateRoot : Entity
{
    private readonly List<DomainEvent> _events = [];
    public IReadOnlyCollection<DomainEvent> Events => new ReadOnlyCollection<DomainEvent>(_events);
    
    protected void RaiseEvent(DomainEvent @event) => _events.Add(@event);
    public void ClearEvents() => _events.Clear();
}