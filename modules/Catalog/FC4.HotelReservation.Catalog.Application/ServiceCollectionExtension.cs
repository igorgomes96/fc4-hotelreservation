using Microsoft.Extensions.DependencyInjection;

namespace FC4.HotelReservation.Catalog.Application;

public static class ServiceCollectionExtension
{
    public static void AddCatalogUseCases(
        this IServiceCollection services)
    {
        services.AddMediatR(x => x.RegisterServicesFromAssemblies(
            typeof(ServiceCollectionExtension).Assembly));
    }
}