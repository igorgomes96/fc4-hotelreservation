using FC4.HotelReservation.Domain.Enums;
using FC4.HotelReservation.Domain.Events;
using FC4.HotelReservation.Domain.Repositories;
using MediatR;

namespace FC4.HotelReservation.Application.EventHandlers;

public class PaymentStatusChangedEventHandler(IReservationRepository reservationRepository)
    : INotificationHandler<PaymentStatusChangedEvent>
{
    public async Task Handle(PaymentStatusChangedEvent notification, CancellationToken cancellationToken)
    {
        var reservation = await reservationRepository.GetByIdAsync(notification.ReservationId, cancellationToken)
                          ?? throw new InvalidOperationException("Reservation not found");

        switch (notification.PaymentStatus)
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