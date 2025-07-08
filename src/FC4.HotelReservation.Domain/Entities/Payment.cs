using Ardalis.GuardClauses;
using FC4.HotelReservation.Domain.Common;
using FC4.HotelReservation.Domain.Enums;
using FC4.HotelReservation.Domain.Events;
using FC4.HotelReservation.Domain.ValueObjects;

namespace FC4.HotelReservation.Domain.Entities;

public class Payment : AggregateRoot
{
    public Guid ReservationId { get; }
    public Money Amount { get; }
    public PaymentStatus Status { get; private set; }
    public DateTime ProcessedAt { get; private set; }
    public string? TransactionId { get; private set; }
    
    private Payment() { } // For EF Core

    public Payment(Guid reservationId, Money amount)
    {
        ReservationId = Guard.Against.Default(reservationId, nameof(reservationId));
        Amount = Guard.Against.Null(amount, nameof(amount));
        Status = PaymentStatus.Pending;
        ProcessedAt = DateTime.UtcNow;
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