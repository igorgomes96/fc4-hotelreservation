using FC4.HotelReservation.Payments.Domain.Events;
using FC4.HotelReservation.Payments.Events.IntegrationEvents;
using MassTransit;
using MediatR;

namespace FC4.HotelReservation.Payments.Application.EventHandlers;

public class PaymentStatusChangedEventHandler(IPublishEndpoint publisher) : INotificationHandler<PaymentStatusChangedEvent>
{
    public async Task Handle(PaymentStatusChangedEvent notification, CancellationToken cancellationToken)
    {
        await publisher.Publish(new PaymentStatusChanged(
                notification.PaymentId,
                notification.ReservationId,
                (PaymentStatusEnum)notification.PaymentStatus),
            cancellationToken);
    }
}