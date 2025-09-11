using Ardalis.GuardClauses;
using FC4.HotelReservation.Guests.Domain.ValueObjects;
using FC4.HotelReservation.Shared.Domain;

namespace FC4.HotelReservation.Guests.Domain.Entities;

public class Guest : AggregateRoot
{
    public string FirstName { get; }
    public string LastName { get; }
    public Email Email { get; private set; }
    
    private Guest() { } // For EF Core
    
    public Guest(Guid id, string firstName, string lastName, Email email) : base(id)
    {
        FirstName = Guard.Against.NullOrWhiteSpace(firstName, nameof(firstName));
        LastName = Guard.Against.NullOrWhiteSpace(lastName, nameof(lastName));
        Email = Guard.Against.Null(email, nameof(email));
    }
    
    public static Guest Create(string firstName, string lastName, Email email)
    {
        return new Guest(Guid.NewGuid(), firstName, lastName, email);
    }

    public string FullName => $"{FirstName} {LastName}";
}