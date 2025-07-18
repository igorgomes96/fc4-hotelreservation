using FC4.HotelReservation.Catalog.Application;
using FC4.HotelReservation.Catalog.Domain.Repositories;
using FC4.HotelReservation.Catalog.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FC4.HotelReservation.Catalog.Infra.Data;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCatalogRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<IHotelRepository, HotelRepository>()
            .AddScoped<IRoomTypeRepository, RoomTypeRepository>()
            .AddScoped<IRoomRepository, RoomRepository>()
            .AddScoped<IRoomTypeRateRepository, RoomTypeRateRepository>()
            .AddDbContext<CatalogDbContext>((serviceProvider, options) =>
            {
                var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                options.UseNpgsql(configuration.GetConnectionString("HotelReservationDb"));
            });
    }
}