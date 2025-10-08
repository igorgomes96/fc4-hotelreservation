using FC4.HotelReservation.Domain.Entities;
using FC4.HotelReservation.Domain.Repositories;
using FC4.HotelReservation.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace FC4.HotelReservation.Infrastructure.Repositories;

public class RoomTypeInventoryRepository(HotelDbContext context) : IRoomTypeInventoryRepository
{
    public async Task<List<RoomTypeInventory>> GetInventoryForPeriodAsync(
        Guid hotelId, 
        Guid roomTypeId, 
        DateRange period, 
        CancellationToken cancellationToken)
    {
        var dates = period.GetDates().ToList();
        
        return await context.RoomTypeInventories
            .Where(i => i.HotelId == hotelId && 
                        i.RoomTypeId == roomTypeId &&
                        dates.Contains(i.Date))
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(RoomTypeInventory inventory, CancellationToken cancellationToken)
    {
        context.RoomTypeInventories.Update(inventory);
        await Task.CompletedTask;
    }
}