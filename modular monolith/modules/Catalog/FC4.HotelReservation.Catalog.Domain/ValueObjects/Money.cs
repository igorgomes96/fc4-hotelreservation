using Ardalis.GuardClauses;

namespace FC4.HotelReservation.Catalog.Domain.ValueObjects;

public record Money(decimal Value, string Currency)
{
    public decimal Value { get; } = Value;
    public string Currency { get; } = Guard.Against.NullOrWhiteSpace(Currency, nameof(Currency));
}