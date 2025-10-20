using FC4.HotelReservation.Reservations.Consumers.Consumers;
using Microsoft.Extensions.DependencyInjection;

namespace FC4.HotelReservation.Reservations.Consumers;

public static class ServiceRegistrationExtensions
{
    public static IServiceCollection AddReservationsConsumers(this IServiceCollection services)
    {
        return services.AddHostedService<PaymentChangedConsumer>();;
    }
}