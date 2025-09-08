using FC4.HotelReservation.Catalog.Domain.Repositories;
using FC4.HotelReservation.Catalog.Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FC4.HotelReservation.Catalog.Infra;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCatalogRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<IHotelRepository, HotelRepository>()
            .AddScoped<IRoomTypeRepository, RoomTypeRepository>()
            .AddScoped<IRoomRepository, RoomRepository>()
            .AddScoped<IRoomTypeRateRepository, RoomTypeRateRepository>();
    }
}