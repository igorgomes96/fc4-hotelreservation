namespace FC4.HotelReservation.Application.Common.IntegrationEvents;

public abstract class IntegrationEvent
{
    public DateTime CreatedOn { get; } = DateTime.UtcNow;
}