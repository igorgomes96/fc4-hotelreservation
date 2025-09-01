using Microsoft.Extensions.DependencyInjection;

namespace FC4.HotelReservation.Application;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddUseCases(
        this IServiceCollection services)
    {
        return services.AddMediatR(x => x.RegisterServicesFromAssemblies(
            typeof(ServiceCollectionExtension).Assembly));
    }
}