using FC4.HotelReservation.Domain.Services;
using FC4.HotelReservation.Domain.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FC4.HotelReservation.Domain;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        return services.AddTransient<IRateService, RateService>();
    }
}