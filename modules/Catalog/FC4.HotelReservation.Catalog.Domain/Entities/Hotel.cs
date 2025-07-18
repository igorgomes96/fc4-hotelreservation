using Ardalis.GuardClauses;
using FC4.HotelReservation.Catalog.Domain.ValueObjects;
using FC4.HotelReservation.Shared.Domain;

namespace FC4.HotelReservation.Catalog.Domain.Entities;

public class Hotel : AggregateRoot
{
    public string Name { get; private set; }
    public Address Address { get; private set; }
    
    private Hotel() { } // For EF Core
    public Hotel(string name, Address address)
    {
        Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));
        Address = Guard.Against.Null(address, nameof(address));
    }

    public void UpdateAddress(Address address)
    {
        Address = Guard.Against.Null(address, nameof(address));
    }
}