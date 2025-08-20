using Ardalis.GuardClauses;
using FC4.HotelReservation.Shared.Domain;

namespace FC4.HotelReservation.Catalog.Domain.Entities;

public class RoomType : AggregateRoot
{
    public string Description { get; }
    
    private RoomType() { } // For EF Core
    
    public RoomType(Guid id, string description) : base(id)
    {
        Description = Guard.Against.NullOrWhiteSpace(description, nameof(description));
    }
    
    public static RoomType Create(string description)
    {
        return new RoomType(Guid.NewGuid(), description);
    }
}