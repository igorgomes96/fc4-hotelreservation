using FC4.HotelReservation.Shared.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FC4.HotelReservation.Shared.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddDbContext<HotelDbContext>((serviceProvider, options) =>
            {
                var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                options.UseNpgsql(configuration.GetConnectionString("HotelReservationDb"));
            });
    }
}