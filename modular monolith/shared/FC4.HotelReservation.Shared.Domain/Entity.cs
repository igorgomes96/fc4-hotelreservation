using Ardalis.GuardClauses;

namespace FC4.HotelReservation.Shared.Domain;

public abstract class Entity
{
    protected Entity() {}
    protected Entity(Guid id)
    {
        Id = Guard.Against.Default(id, nameof(id));
    }
    
    public Guid Id { get; }
}