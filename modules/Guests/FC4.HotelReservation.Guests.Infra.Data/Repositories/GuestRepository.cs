using FC4.HotelReservation.Guests.Domain.Entities;
using FC4.HotelReservation.Guests.Domain.Repositories;
using FC4.HotelReservation.Shared.Infrastructure;

namespace FC4.HotelReservation.Guests.Infra.Repositories;

public class GuestRepository(HotelDbContext context) : IGuestRepository
{
    public async Task CreateGuestAsync(Guest guest, CancellationToken cancellationToken)
    {
        await context.Guests.AddAsync(guest, cancellationToken);
    }
}