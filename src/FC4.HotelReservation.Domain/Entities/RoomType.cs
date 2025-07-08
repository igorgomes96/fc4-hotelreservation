using Ardalis.GuardClauses;
using FC4.HotelReservation.Domain.Common;

namespace FC4.HotelReservation.Domain.Entities;

public class RoomType : AggregateRoot
{
    public string Description { get; }
    
    private RoomType() { } // For EF Core
    
    public RoomType(string description)
    {
        Description = Guard.Against.NullOrWhiteSpace(description, nameof(description));
    }
}