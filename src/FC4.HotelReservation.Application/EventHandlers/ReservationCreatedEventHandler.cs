using FC4.HotelReservation.Domain.Entities;
using FC4.HotelReservation.Domain.Events;
using FC4.HotelReservation.Domain.Repositories;
using MediatR;

namespace FC4.HotelReservation.Application.EventHandlers;

public class ReservationCreatedEventHandler(IPaymentRepository paymentRepository)
    : INotificationHandler<ReservationCreatedEvent>
{
    public async Task Handle(ReservationCreatedEvent notification, CancellationToken cancellationToken)
    {
        var payment = new Payment(notification.ReservationId, notification.Amount);
        await paymentRepository.CreateAsync(payment, cancellationToken);
    }
}