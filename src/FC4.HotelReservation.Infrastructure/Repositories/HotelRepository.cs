using FC4.HotelReservation.Domain.Entities;
using FC4.HotelReservation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FC4.HotelReservation.Infrastructure.Repositories;

public class HotelRepository(HotelDbContext context) : IHotelRepository
{
    public async Task<Hotel?> GetByIdAsync(Guid hotelId, CancellationToken cancellationToken)
    {
        return await context.Hotels
            .SingleOrDefaultAsync(h => h.Id == hotelId, cancellationToken);
    }

    public async Task CreateHotelAsync(Hotel hotel, CancellationToken cancellationToken)
    {
        await context.Hotels.AddAsync(hotel, cancellationToken);
    }
}