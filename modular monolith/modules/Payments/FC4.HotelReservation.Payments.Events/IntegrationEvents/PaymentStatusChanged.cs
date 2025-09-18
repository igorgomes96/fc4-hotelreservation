using FC4.HotelReservation.Shared.Application.IntegrationEvents;

namespace FC4.HotelReservation.Payments.Events.IntegrationEvents;

public class PaymentStatusChanged(Guid paymentId, Guid reservationId, PaymentStatusEnum paymentStatus) : IntegrationEvent
{
    public Guid PaymentId { get; } = paymentId;
    public Guid ReservationId { get; } = reservationId;
    public PaymentStatusEnum PaymentStatus { get; } = paymentStatus;
}

public enum PaymentStatusEnum
{
    Pending = 1,
    Processing = 2,
    Completed = 3,
    Failed = 4,
    Refunded = 5
}