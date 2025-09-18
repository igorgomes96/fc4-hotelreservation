using FC4.HotelReservation.Payments.Domain.Enums;
using FC4.HotelReservation.Shared.Domain;

namespace FC4.HotelReservation.Payments.Domain.Events;

public class PaymentStatusChangedEvent(Guid paymentId, Guid reservationId, PaymentStatus paymentStatus)
    : DomainEvent
{
    public Guid PaymentId { get; } = paymentId;
    public Guid ReservationId { get; } = reservationId;
    public PaymentStatus PaymentStatus { get; } = paymentStatus;
}