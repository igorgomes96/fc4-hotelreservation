using FC4.HotelReservation.Catalog.Domain.Entities;
using FC4.HotelReservation.Catalog.Domain.ValueObjects;

namespace FC4.HotelReservation.Catalog.Domain.Repositories;

public interface IRoomTypeRateRepository
{
    Task<IEnumerable<RoomTypeRate>> GetRateForPeriodAsync(
        Guid hotelId, 
        Guid roomTypeId, 
        DateRange period, 
        CancellationToken cancellationToken);
}