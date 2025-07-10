using FC4.HotelReservation.Domain.Entities;
using FC4.HotelReservation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FC4.HotelReservation.Infrastructure.Repositories;

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