using FC4.HotelReservation.Domain.Entities;
using FC4.HotelReservation.Domain.ValueObjects;

namespace FC4.HotelReservation.Domain.Repositories;

public interface IRoomTypeRateRepository
{
    Task<IEnumerable<RoomTypeRate>> GetRateForPeriodAsync(
        Guid hotelId, 
        Guid roomTypeId, 
        DateRange period, 
        CancellationToken cancellationToken);
}