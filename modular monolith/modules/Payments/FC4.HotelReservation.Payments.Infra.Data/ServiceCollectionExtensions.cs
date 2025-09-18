using FC4.HotelReservation.Payments.Domain.Repositories;
using FC4.HotelReservation.Payments.Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FC4.HotelReservation.Payments.Infra;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPaymentsRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<IPaymentRepository, PaymentRepository>();
    }
}