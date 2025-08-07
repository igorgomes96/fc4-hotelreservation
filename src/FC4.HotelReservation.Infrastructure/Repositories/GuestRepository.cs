using FC4.HotelReservation.Domain.Entities;
using FC4.HotelReservation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FC4.HotelReservation.Infrastructure.Repositories;

public class GuestRepository(HotelDbContext context) : IGuestRepository
{
    public async Task<Guest?> GetByIdAsync(Guid hotelId, CancellationToken cancellationToken)
    {
        return await context.Guests
            .SingleOrDefaultAsync(g => g.Id == hotelId, cancellationToken);
    }

    public async Task CreateGuestAsync(Guest guest, CancellationToken cancellationToken)
    {
        await context.Guests.AddAsync(guest, cancellationToken);
    }
}