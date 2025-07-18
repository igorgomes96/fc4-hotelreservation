using FC.HotelReservation.Payments.Application;
using FC4.HotelReservation.Payments.Domain.Repositories;
using FC4.HotelReservation.Payments.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FC4.HotelReservation.Payments.Infra.Data;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPaymentRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<IPaymentRepository, PaymentRepository>()
            .AddDbContext<PaymentsDbContext>((serviceProvider, options) =>
            {
                var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                options.UseNpgsql(configuration.GetConnectionString("HotelReservationDb"));
            });
    }
}