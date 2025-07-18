using Microsoft.Extensions.DependencyInjection;

namespace FC4.HotelReservation.Payments.Application;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddPaymentsUseCases(
        this IServiceCollection services)
    {
        return services.AddMediatR(x => x.RegisterServicesFromAssemblies(
            typeof(ServiceCollectionExtension).Assembly));
    }
}