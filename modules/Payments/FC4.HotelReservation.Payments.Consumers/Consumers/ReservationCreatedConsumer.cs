using FC4.HotelReservation.Payments.Application.UseCases.Payment.CreatePendingPayment;
using FC4.HotelReservation.Reservations.Events.IntegrationEvents;
using MassTransit;
using MediatR;

namespace FC4.HotelReservation.Payments.Consumers.Consumers;

public class ReservationCreatedConsumer(IMediator mediator) : IConsumer<ReservationCreated>
{
    public async Task Consume(ConsumeContext<ReservationCreated> context)
    {
        await mediator.Send(new CreatePendingPaymentInput(
                context.Message.ReservationId,
                context.Message.Amount,
                context.Message.Currency),
            context.CancellationToken);
    }
}