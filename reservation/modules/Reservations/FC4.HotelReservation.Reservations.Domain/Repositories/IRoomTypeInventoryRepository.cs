using FC4.HotelReservation.Reservations.Domain.Entities;
using FC4.HotelReservation.Reservations.Domain.ValueObjects;

namespace FC4.HotelReservation.Reservations.Domain.Repositories;

public interface IRoomTypeInventoryRepository
{
    Task<List<RoomTypeInventory>> GetInventoryForPeriodAsync(
        Guid hotelId, 
        Guid roomTypeId, 
        DateRange period, 
        CancellationToken cancellationToken);
    
    Task UpdateAsync(RoomTypeInventory inventory, CancellationToken cancellationToken);
}