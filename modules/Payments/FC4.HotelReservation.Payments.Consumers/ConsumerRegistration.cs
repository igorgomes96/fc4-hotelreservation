using FC4.HotelReservation.Payments.Consumers.Consumers;
using MassTransit;

namespace FC4.HotelReservation.Payments.Consumers;

public static class ConsumerRegistration
{
    public static IBusRegistrationConfigurator AddPaymentConsumers(this IBusRegistrationConfigurator configurator)
    {
        configurator.AddConsumer<ReservationCreatedConsumer>();
        return configurator;
    }
}