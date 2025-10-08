using FC4.HotelReservation.Catalog.Domain.Entities;
using FC4.HotelReservation.Catalog.Domain.Repositories;
using FC4.HotelReservation.Catalog.Domain.ValueObjects;
using FC4.HotelReservation.Shared.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FC4.HotelReservation.Catalog.Infra.Repositories;

public class RoomTypeRateRepository(HotelDbContext context) : IRoomTypeRateRepository
{
    public async Task<IEnumerable<RoomTypeRate>> GetRateForPeriodAsync(
        Guid hotelId, 
        Guid roomTypeId, 
        DateRange period, 
        CancellationToken cancellationToken)
    {
        return await context.RoomTypeRates
            .Where(rtr => rtr.HotelId == hotelId 
                          && rtr.RoomTypeId == roomTypeId
                          && rtr.Date >= period.StartDate 
                          && rtr.Date <= period.EndDate)
            .OrderBy(rtr => rtr.Date)
            .ToListAsync(cancellationToken);
    }
}