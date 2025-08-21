using Ardalis.GuardClauses;
using FC4.HotelReservation.Shared.Domain;
using FC4.HotelReservation.Shared.Domain.ValueObjects;

namespace FC4.HotelReservation.Domain.Entities;

public class GuestInfo : AggregateRoot
{
    public string FirstName { get; }
    public string LastName { get; }
    public Email Email { get; private set; }
    
    private GuestInfo() { } // For EF Core
    
    public GuestInfo(Guid id, string firstName, string lastName, Email email) : base(id)
    {
        FirstName = Guard.Against.NullOrWhiteSpace(firstName, nameof(firstName));
        LastName = Guard.Against.NullOrWhiteSpace(lastName, nameof(lastName));
        Email = Guard.Against.Null(email, nameof(email));
    }
    
    public static GuestInfo Create(string firstName, string lastName, Email email)
    {
        return new GuestInfo(Guid.NewGuid(), firstName, lastName, email);
    }

    public string FullName => $"{FirstName} {LastName}";
}