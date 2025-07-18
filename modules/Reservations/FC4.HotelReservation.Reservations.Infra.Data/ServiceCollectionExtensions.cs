using FC4.HotelReservation.Reservations.Application;
using FC4.HotelReservation.Reservations.Domain.Repositories;
using FC4.HotelReservation.Reservations.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FC4.HotelReservation.Reservations.Infra.Data;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddReservationRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<IReservationRepository, ReservationRepository>()
            .AddScoped<IRoomTypeInventoryRepository, RoomTypeInventoryRepository>()
            .AddDbContext<ReservationsDbContext>((serviceProvider, options) =>
            {
                var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                options.UseNpgsql(configuration.GetConnectionString("HotelReservationDb"));
            });
    }
}