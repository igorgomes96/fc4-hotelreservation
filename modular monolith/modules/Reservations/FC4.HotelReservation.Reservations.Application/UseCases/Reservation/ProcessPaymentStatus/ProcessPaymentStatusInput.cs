using MediatR;

namespace FC4.HotelReservation.Reservations.Application.UseCases.Reservation.ProcessPaymentStatus;

public record ProcessPaymentStatusInput(Guid PaymentId, Guid ReservationId, PaymentStatus PaymentStatus)
    : IRequest;

public enum PaymentStatus
{
    Pending = 1,
    Processing = 2,
    Completed = 3,
    Failed = 4,
    Refunded = 5
}