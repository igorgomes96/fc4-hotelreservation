using FC4.HotelReservation.Domain.Enums;

namespace FC4.HotelReservation.Application.Services;

public interface IReservationsGateway
{
    Task ProcessPaymentStatusChangedAsync(
        Guid reservationId,
        Guid paymentId,
        PaymentStatus paymentStatus,
        CancellationToken cancellationToken = default);
}