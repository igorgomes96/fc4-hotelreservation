using FC4.HotelReservation.Payments.Events.IntegrationEvents;
using FC4.HotelReservation.Reservations.Application.UseCases.Reservation.ProcessPaymentStatus;
using MassTransit;
using MediatR;

namespace FC4.HotelReservation.Reservations.Consumers.Consumers;

public class PaymentStatusChangedConsumer(IMediator mediator) : IConsumer<PaymentStatusChanged>
{
    public async Task Consume(ConsumeContext<PaymentStatusChanged> context)
    {
        await mediator.Send(new ProcessPaymentStatusInput(
                context.Message.PaymentId,
                context.Message.ReservationId,
                (PaymentStatus)context.Message.PaymentStatus),
            context.CancellationToken);
    }
}