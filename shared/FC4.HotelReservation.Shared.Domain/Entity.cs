namespace FC4.HotelReservation.Shared.Domain;

public abstract class Entity
{
    public Guid Id { get; internal set; } = Guid.NewGuid();
}