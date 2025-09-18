using FC4.HotelReservation.Reservations.Consumers.Consumers;
using MassTransit;

namespace FC4.HotelReservation.Reservations.Consumers;

public static class ConsumerRegistration
{
    public static IBusRegistrationConfigurator AddReservationConsumers(this IBusRegistrationConfigurator configurator)
    {
        configurator.AddConsumer<PaymentStatusChangedConsumer>();
        return configurator;
    }
}