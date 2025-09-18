using Microsoft.Extensions.DependencyInjection;

namespace FC4.HotelReservation.Guests.Application;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddGuestsUseCases(
        this IServiceCollection services)
    {
        services.AddMediatR(x => x.RegisterServicesFromAssemblies(
            typeof(ServiceCollectionExtension).Assembly));
        return services;
    }
}