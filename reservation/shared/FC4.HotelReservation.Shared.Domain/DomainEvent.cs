using MediatR;

namespace FC4.HotelReservation.Shared.Domain;

public abstract class DomainEvent : INotification
{
    public DateTime OccuredOn { get; private set; } = DateTime.UtcNow;
}