using FC4.HotelReservation.Domain.Enums;
using FC4.HotelReservation.Domain.Repositories;

namespace FC4.HotelReservation.Application.Services;

public class ReservationsGateway(IReservationRepository reservationRepository) : IReservationsGateway
{
    public async Task ProcessPaymentStatusChangedAsync(
        Guid reservationId,
        Guid paymentId,
        PaymentStatus paymentStatus,
        CancellationToken cancellationToken = default)
    {
        var reservation = await reservationRepository.GetByIdAsync(reservationId, cancellationToken)
                          ?? throw new InvalidOperationException("Reservation not found");

        switch (paymentStatus)
        {
            case PaymentStatus.Completed:
                reservation.MarkAsPaid();
                break;
            case PaymentStatus.Failed:
                reservation.Reject();
                break;
            case PaymentStatus.Refunded:
                reservation.Cancel();
                break;
            case PaymentStatus.Pending:
            case PaymentStatus.Processing:
            default:
                return;
        }

        await reservationRepository.UpdateAsync(reservation, cancellationToken);
    }
}