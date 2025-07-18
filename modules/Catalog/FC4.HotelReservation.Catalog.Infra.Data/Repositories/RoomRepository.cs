using FC4.HotelReservation.Catalog.Domain.Entities;
using FC4.HotelReservation.Catalog.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FC4.HotelReservation.Catalog.Infra.Data.Repositories;

public class RoomRepository(CatalogDbContext context) : IRoomRepository
{
    public async Task<Room?> GetByIdAsync(Guid roomId, CancellationToken cancellationToken)
    {
        return await context.Rooms
            .FirstOrDefaultAsync(r => r.Id == roomId, cancellationToken);
    }

    public async Task CreateAsync(Room room, CancellationToken cancellationToken)
    {
        await context.Rooms.AddAsync(room, cancellationToken);
    }
}
