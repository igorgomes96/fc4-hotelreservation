using FC4.HotelReservation.Reservations.Events.IntegrationEvents;
using MassTransit;

namespace FC4.HotelReservation.Payments.Consumers.Consumers;

public class ReservationCreatedConsumer : IConsumer<ReservationCreated>
{
    public Task Consume(ConsumeContext<ReservationCreated> context)
    {
        return Task.CompletedTask;
    }
}