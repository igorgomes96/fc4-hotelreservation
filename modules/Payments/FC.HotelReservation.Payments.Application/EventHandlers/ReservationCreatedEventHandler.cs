using FC4.HotelReservation.Payments.Domain.Entities;
using FC4.HotelReservation.Payments.Domain.Repositories;
using FC4.HotelReservation.Reservations.Domain.Events;
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