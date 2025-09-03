namespace FC4.HotelReservation.Shared.Application.IntegrationEvents;

public abstract class IntegrationEvent
{
    public DateTime CreatedOn { get; } = DateTime.UtcNow;
}