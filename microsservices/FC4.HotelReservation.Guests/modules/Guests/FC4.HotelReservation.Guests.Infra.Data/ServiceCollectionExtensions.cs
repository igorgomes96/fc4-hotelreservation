using FC4.HotelReservation.Guests.Domain.Repositories;
using FC4.HotelReservation.Guests.Infra.Repositories;
using FC4.HotelReservation.Shared.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FC4.HotelReservation.Guests.Infra;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGuestsRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddDbContext<HotelDbContext>((serviceProvider, options) =>
            {
                var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                options.UseNpgsql(configuration.GetConnectionString("HotelReservationDb"));
            })
            .AddScoped<IGuestRepository, GuestRepository>();
    }
}