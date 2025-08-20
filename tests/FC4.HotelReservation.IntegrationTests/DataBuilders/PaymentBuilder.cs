using Bogus;
using FC4.HotelReservation.Domain.Enums;
using FC4.HotelReservation.Shared.Domain.ValueObjects;

namespace FC4.HotelReservation.IntegrationTests.DataBuilders;

public class PaymentBuilder
{
    private readonly Faker _faker = new();
    private Guid _id = Guid.NewGuid();
    private Guid _reservationId = Guid.NewGuid();
    private decimal _amount;
    private PaymentStatus _status = PaymentStatus.Pending;
    private string? _transactionId;

    public PaymentBuilder()
    {
        _amount = _faker.Random.Decimal(100, 5000);
    }

    public static PaymentBuilder APayment() => new();

    public PaymentBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public PaymentBuilder WithReservationId(Guid reservationId)
    {
        _reservationId = reservationId;
        return this;
    }

    public PaymentBuilder WithAmount(decimal amount)
    {
        _amount = amount;
        return this;
    }

    public PaymentBuilder WithStatus(PaymentStatus status)
    {
        _status = status;
        return this;
    }

    public PaymentBuilder WithTransactionId(string? transactionId)
    {
        _transactionId = transactionId;
        return this;
    }

    public Domain.Entities.Payment Build()
    {
        var transactionId = _transactionId ?? _faker.Random.AlphaNumeric(10);
        return new Domain.Entities.Payment(_id, _reservationId, new Money(_amount, "BRL"),
            _status, DateTime.UtcNow, transactionId);
    }
}