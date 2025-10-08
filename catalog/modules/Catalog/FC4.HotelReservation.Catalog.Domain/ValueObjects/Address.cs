using Ardalis.GuardClauses;

namespace FC4.HotelReservation.Catalog.Domain.ValueObjects;

public record Address(
    string Street,
    string City,
    string State,
    string Country,
    string ZipCode)
{
    public string Street { get; } = Guard.Against.NullOrWhiteSpace(Street, nameof(Street));
    public string City { get; } = Guard.Against.NullOrWhiteSpace(City, nameof(City));
    public string State { get; } = Guard.Against.NullOrWhiteSpace(State, nameof(State));
    public string Country { get; } = Guard.Against.NullOrWhiteSpace(Country, nameof(Country));
    public string ZipCode { get; } = Guard.Against.NullOrWhiteSpace(ZipCode, nameof(ZipCode));
}
