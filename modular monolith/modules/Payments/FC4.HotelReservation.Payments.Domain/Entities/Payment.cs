using Ardalis.GuardClauses;
using FC4.HotelReservation.Payments.Domain.Enums;
using FC4.HotelReservation.Payments.Domain.Events;
using FC4.HotelReservation.Payments.Domain.ValueObjects;
using FC4.HotelReservation.Shared.Domain;

namespace FC4.HotelReservation.Payments.Domain.Entities;

public class Payment : AggregateRoot
{
    public Guid ReservationId { get; }
    public Money Amount { get; }
    public PaymentStatus Status { get; private set; }
    public DateTime ProcessedAt { get; private set; }
    public string? TransactionId { get; private set; }

    private Payment()
    {
    } // For EF Core

    public Payment(Guid id, Guid reservationId, Money amount, PaymentStatus status, DateTime processedAt,
        string? transactionId) : base(id)
    {
        ReservationId = Guard.Against.Default(reservationId, nameof(reservationId));
        Amount = Guard.Against.Null(amount, nameof(amount));
        Status = Guard.Against.EnumOutOfRange(status, nameof(status));
        ProcessedAt = Guard.Against.Default(processedAt, nameof(processedAt));
        TransactionId = transactionId;
    }

    public static Payment Create(Guid reservationId, Money amount)
    {
        return new Payment(Guid.NewGuid(),
            reservationId,
            amount,
            PaymentStatus.Pending,
            DateTime.UtcNow,
            null);
    }

    private void SendPaymentStatusChangedEvent()
    {
        RaiseEvent(new PaymentStatusChangedEvent(Id, ReservationId, Status));
    }

    public void MarkAsProcessing(string transactionId)
    {
        TransactionId = Guard.Against.NullOrWhiteSpace(transactionId, nameof(transactionId));
        Status = PaymentStatus.Processing;
        ProcessedAt = DateTime.UtcNow;
        SendPaymentStatusChangedEvent();
    }

    public void MarkAsCompleted()
    {
        if (Status != PaymentStatus.Processing)
            throw new InvalidOperationException("Payment must be in processing status");

        Status = PaymentStatus.Completed;
        SendPaymentStatusChangedEvent();
    }

    public void MarkAsFailed()
    {
        if (Status != PaymentStatus.Processing)
            throw new InvalidOperationException("Payment must be in processing status");

        Status = PaymentStatus.Failed;
        SendPaymentStatusChangedEvent();
    }

    public void Refund()
    {
        if (Status != PaymentStatus.Completed)
            throw new InvalidOperationException("We can only refund completed payments");

        Status = PaymentStatus.Refunded;
        SendPaymentStatusChangedEvent();
    }
}