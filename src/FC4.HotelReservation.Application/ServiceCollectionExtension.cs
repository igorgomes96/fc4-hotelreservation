using Microsoft.Extensions.DependencyInjection;

namespace FC4.HotelReservation.Application;

public static class ServiceCollectionExtension
{
    public static void AddUseCases(
        this IServiceCollection services)
    {
        services.AddMediatR(x => x.RegisterServicesFromAssemblies(
            typeof(ServiceCollectionExtension).Assembly));
    }
}