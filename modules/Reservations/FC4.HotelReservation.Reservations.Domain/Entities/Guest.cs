using Ardalis.GuardClauses;
using FC4.HotelReservation.Shared.Domain;
using FC4.HotelReservation.Shared.Domain.ValueObjects;

namespace FC4.HotelReservation.Reservations.Domain.Entities;

public class Guest : AggregateRoot
{
    public string FirstName { get; }
    public string LastName { get; }
    public Email Email { get; private set; }
    
    private Guest() { } // For EF Core
    
    public Guest(string firstName, string lastName, Email email)
    {
        FirstName = Guard.Against.NullOrWhiteSpace(firstName, nameof(firstName));
        LastName = Guard.Against.NullOrWhiteSpace(lastName, nameof(lastName));
        Email = Guard.Against.Null(email, nameof(email));
    }
    
    public void UpdateEmail(Email email)
    {
        Email = Guard.Against.Null(email, nameof(email));
    }

    public string FullName => $"{FirstName} {LastName}";
}