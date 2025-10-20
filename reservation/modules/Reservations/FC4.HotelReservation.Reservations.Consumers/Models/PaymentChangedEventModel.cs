using FC4.HotelReservation.Reservations.Application.UseCases.Reservation.ProcessPaymentStatus;
using FC4.HotelReservation.Reservations.Domain.Enums;

namespace FC4.HotelReservation.Reservations.Consumers.Models;

public class PaymentChangedEventModel
{
    public string Op { get; set; }
    public PaymentChangedModel? Before { get; set; }
    public PaymentChangedModel? After { get; set; }
}

public class PaymentChangedModel
{
    public Guid Id { get; set; }
    public Guid ReservationId { get; set; }
    public string Status { get; set; }
    
    private PaymentStatus MapPaymentStatus()
    {
        return Status.ToLower() switch
        {
            "pending" => PaymentStatus.Pending,
            "processing" => PaymentStatus.Processing,
            "completed" => PaymentStatus.Completed,
            "failed" => PaymentStatus.Failed,
            "refunded" => PaymentStatus.Refunded,
            _ => throw new ArgumentOutOfRangeException($"Unknown payment status: {Status}")
        };
    }
    
    public ProcessPaymentStatusInput ToProcessPaymentStatusInput()
    {
        return new ProcessPaymentStatusInput(
            Id, ReservationId, MapPaymentStatus());
    }
}