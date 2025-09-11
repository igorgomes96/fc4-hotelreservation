using FC4.HotelReservation.Catalog.Domain.Entities;
using FC4.HotelReservation.Catalog.Domain.Repositories;
using FC4.HotelReservation.Shared.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FC4.HotelReservation.Catalog.Infra.Repositories;

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