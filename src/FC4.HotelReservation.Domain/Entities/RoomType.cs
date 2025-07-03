using Ardalis.GuardClauses;
using FC4.HotelReservation.Domain.Common;

namespace FC4.HotelReservation.Domain.Entities;

public class RoomType : AggregateRoot
{
    public string Description { get; }
    
    public RoomType(string description)
    {
        Description = Guard.Against.NullOrWhiteSpace(description, nameof(description));
    }
}