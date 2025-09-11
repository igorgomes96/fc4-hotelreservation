using FC4.HotelReservation.Catalog.Domain.Entities;
using FC4.HotelReservation.Catalog.Domain.Repositories;
using FC4.HotelReservation.Shared.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FC4.HotelReservation.Catalog.Infra.Repositories;

public class RoomTypeRepository(HotelDbContext context): IRoomTypeRepository
{
    public async Task<RoomType?> GetByIdAsync(Guid roomTypeId, CancellationToken cancellationToken)
    {
        return await context.RoomTypes
            .FirstOrDefaultAsync(r => r.Id == roomTypeId, cancellationToken);
    }

    public async Task CreateAsync(RoomType roomType, CancellationToken cancellationToken)
    {
        await context.RoomTypes.AddAsync(roomType, cancellationToken);
    }
}