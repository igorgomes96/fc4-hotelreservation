using FC4.HotelReservation.Reservations.Domain.Events;
using FC4.HotelReservation.Reservations.Events.IntegrationEvents;
using MassTransit;
using MediatR;

namespace FC4.HotelReservation.Reservations.Application.EventHandlers;

public class ReservationCreatedEventHandler(IPublishEndpoint publishEndpoint)
    : INotificationHandler<ReservationCreatedEvent>
{
    public async Task Handle(ReservationCreatedEvent notification, CancellationToken cancellationToken)
    {
        var integrationEvent = new ReservationCreated(
            notification.ReservationId,
            notification.Amount.Value,
            notification.Amount.Currency);
        await publishEndpoint.Publish(integrationEvent, cancellationToken);
    }
}