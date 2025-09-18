using Ardalis.GuardClauses;

namespace FC4.HotelReservation.Reservations.Domain.ValueObjects;

public record Email(string Value)
{
    private const string EmailRegex = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
    public string Value { get; } = Guard.Against.InvalidFormat(Value, nameof(Value), EmailRegex);

    public static implicit operator string(Email email) => email.Value;

    public static implicit operator Email(string email) => new Email(email);

}