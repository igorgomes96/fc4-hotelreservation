using Ardalis.GuardClauses;
using FC4.HotelReservation.Catalog.Domain.ValueObjects;
using FC4.HotelReservation.Shared.Domain;

namespace FC4.HotelReservation.Catalog.Domain.Entities;

public class Hotel : AggregateRoot
{
    public string Name { get; private set; }
    public Address Address { get; private set; }
    
    private Hotel() { } // For EF Core
    public Hotel(Guid id, string name, Address address) : base(id)
    {
        Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));
        Address = Guard.Against.Null(address, nameof(address));
    }
    
    public static Hotel Create(string name, Address address)
    {
        return new Hotel(Guid.NewGuid(), name, address);
    }
}