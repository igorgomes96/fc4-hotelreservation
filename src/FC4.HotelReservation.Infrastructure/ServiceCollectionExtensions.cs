using FC4.HotelReservation.Infrastructure.Repositories;
using FC4.HotelReservation.Payments.Domain.Repositories;
using FC4.HotelReservation.Reservations.Domain.Repositories;
using FC4.HotelReservation.Shared.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FC4.HotelReservation.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<IPaymentRepository, PaymentRepository>()
            .AddScoped<IReservationRepository, ReservationRepository>()
            .AddScoped<IRoomTypeInventoryRepository, RoomTypeInventoryRepository>()
            .AddDbContext<HotelDbContext>((serviceProvider, options) =>
            {
                var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                options.UseNpgsql(configuration.GetConnectionString("HotelReservationDb"));
            });
    }
}