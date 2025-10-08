using FC4.HotelReservation.Domain.Common;
using FC4.HotelReservation.Domain.Enums;

namespace FC4.HotelReservation.Domain.Events;

public class PaymentStatusChangedEvent(Guid paymentId, Guid reservationId, PaymentStatus paymentStatus)
    : DomainEvent
{
    public Guid PaymentId { get; } = paymentId;
    public Guid ReservationId { get; } = reservationId;
    public PaymentStatus PaymentStatus { get; } = paymentStatus;
}