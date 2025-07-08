namespace FC4.HotelReservation.Domain.Common;

public abstract class Entity
{
    public Guid Id { get; internal set; } = Guid.NewGuid();
}