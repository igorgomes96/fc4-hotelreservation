using FC4.HotelReservation.Reservations.Domain.Repositories;
using FC4.HotelReservation.Reservations.Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FC4.HotelReservation.Reservations.Infra.Data;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddReservationsRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<IReservationRepository, ReservationRepository>()
            .AddScoped<IReservationGuestRepository, ReservationGuestRepository>()
            .AddScoped<IRoomTypeInventoryRepository, RoomTypeInventoryRepository>();
    }
}