using FC4.HotelReservation.Catalog.Domain.Repositories;
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
            .AddScoped<IHotelRepository, HotelRepository>()
            .AddScoped<IPaymentRepository, PaymentRepository>()
            .AddScoped<IRoomTypeRepository, RoomTypeRepository>()
            .AddScoped<IReservationRepository, ReservationRepository>()
            .AddScoped<IRoomRepository, RoomRepository>()
            .AddScoped<IRoomTypeRateRepository, RoomTypeRateRepository>()
            .AddScoped<IRoomTypeInventoryRepository, RoomTypeInventoryRepository>()
            .AddDbContext<HotelDbContext>((serviceProvider, options) =>
            {
                var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                options.UseNpgsql(configuration.GetConnectionString("HotelReservationDb"));
            });
    }
}