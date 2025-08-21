using Ardalis.GuardClauses;
using FC4.HotelReservation.Shared.Domain;
using FC4.HotelReservation.Shared.Domain.ValueObjects;

namespace FC4.HotelReservation.Domain.Entities;

public class GuestInfo : AggregateRoot
{
    public Email Email { get; private set; }
    public string FullName { get; private set; }
    
    private GuestInfo() { } // For EF Core
    
    public GuestInfo(Guid id, string fullName, Email email) : base(id)
    {
        FullName = Guard.Against.NullOrEmpty(fullName, nameof(fullName));
        Email = Guard.Against.Null(email, nameof(email));
    }

}