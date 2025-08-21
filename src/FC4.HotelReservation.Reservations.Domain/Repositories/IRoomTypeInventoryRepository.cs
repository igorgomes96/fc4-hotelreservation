using FC4.HotelReservation.Domain.Entities;
using FC4.HotelReservation.Shared.Domain.ValueObjects;

namespace FC4.HotelReservation.Domain.Repositories;

public interface IRoomTypeInventoryRepository
{
    Task<List<RoomTypeInventory>> GetInventoryForPeriodAsync(
        Guid hotelId, 
        Guid roomTypeId, 
        DateRange period, 
        CancellationToken cancellationToken);
    
    Task UpdateAsync(RoomTypeInventory inventory, CancellationToken cancellationToken);
}