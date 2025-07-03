using MediatR;

namespace FC4.HotelReservation.Domain.Common;

public abstract class DomainEvent : INotification
{
    public DateTime OccuredOn { get; private set; } = DateTime.UtcNow;
}