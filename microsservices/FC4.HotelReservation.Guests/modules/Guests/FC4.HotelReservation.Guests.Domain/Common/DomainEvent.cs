using MediatR;

namespace FC4.HotelReservation.Guests.Domain.Common;

public abstract class DomainEvent : INotification
{
    public DateTime OccuredOn { get; private set; } = DateTime.UtcNow;
}