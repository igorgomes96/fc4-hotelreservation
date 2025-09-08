using FC4.HotelReservation.Guests.Domain.Repositories;
using FC4.HotelReservation.Guests.Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FC4.HotelReservation.Guests.Infra;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGuestsRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<IGuestRepository, GuestRepository>();
    }
}