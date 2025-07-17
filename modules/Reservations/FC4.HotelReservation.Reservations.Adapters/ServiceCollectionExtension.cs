using FC4.HotelReservation.Reservations.Adapters.Adapters;
using FC4.HotelReservation.Reservations.Application.Gateway;
using Microsoft.Extensions.DependencyInjection;

namespace FC4.HotelReservation.Reservations.Adapters;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddReservationAdapters(this IServiceCollection services)
    {
        services.AddScoped<ICatalogRateGateway, CatalogRateAdapter>();
        return services;
    }
}