using FC4.HotelReservation.Catalog.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FC4.HotelReservation.Catalog.Domain;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        return services.AddTransient<RateService>();
    }
}