using FC4.HotelReservation.Application.Services;
using FC4.HotelReservation.Domain.Events;
using MediatR;

namespace FC4.HotelReservation.Application.EventHandlers;

public class PaymentStatusChangedEventHandler(IReservationsGateway reservationsGateway)
    : INotificationHandler<PaymentStatusChangedEvent>
{
    public async Task Handle(PaymentStatusChangedEvent notification, CancellationToken cancellationToken)
    {
        await reservationsGateway.ProcessPaymentStatusChangedAsync(
            notification.ReservationId,
            notification.PaymentId,
            notification.PaymentStatus,
            cancellationToken);
    }
}