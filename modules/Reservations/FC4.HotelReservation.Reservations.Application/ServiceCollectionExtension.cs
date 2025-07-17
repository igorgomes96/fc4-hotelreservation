using Microsoft.Extensions.DependencyInjection;

namespace FC4.HotelReservation.Reservations.Application;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddReservationsUseCases(
        this IServiceCollection services)
    {
        return services.AddMediatR(x => x.RegisterServicesFromAssemblies(
            typeof(ServiceCollectionExtension).Assembly));
    }
}