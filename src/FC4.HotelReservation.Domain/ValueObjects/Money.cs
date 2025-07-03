using Ardalis.GuardClauses;

namespace FC4.HotelReservation.Domain.ValueObjects;

public record Money(decimal Amount, string Currency)
{
    public decimal Amount { get; } = Amount;
    public string Currency { get; } = Guard.Against.NullOrWhiteSpace(Currency, nameof(Currency));

    public override string ToString() => $"{Amount} {Currency}";
}