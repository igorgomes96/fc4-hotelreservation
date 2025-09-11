using Bogus;
using FC4.HotelReservation.Payments.Application.UseCases.Payment.UpdatePaymentStatus;
using FC4.HotelReservation.Payments.Domain.Enums;

namespace FC4.HotelReservation.IntegrationTests.DataBuilders;

public class UpdatePaymentStatusInputBuilder
{
    private readonly Faker _faker = new();
    private Guid _paymentId = Guid.NewGuid();
    private PaymentStatus _status = PaymentStatus.Pending;
    private string _transactionId;

    public UpdatePaymentStatusInputBuilder()
    {
        _transactionId = _faker.Random.AlphaNumeric(20);
    }

    public static UpdatePaymentStatusInputBuilder AUpdatePaymentStatusInput()
    {
        return new UpdatePaymentStatusInputBuilder();
    }

    public UpdatePaymentStatusInputBuilder WithPaymentId(Guid paymentId)
    {
        _paymentId = paymentId;
        return this;
    }

    public UpdatePaymentStatusInputBuilder WithStatus(PaymentStatus status)
    {
        _status = status;
        return this;
    }

    public UpdatePaymentStatusInputBuilder WithTransactionId(string transactionId)
    {
        _transactionId = transactionId;
        return this;
    }

    public UpdatePaymentStatusInput Build() => new(
        PaymentId: _paymentId,
        Status: _status,
        TransactionId: _transactionId
    );
}