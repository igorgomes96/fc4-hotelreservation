using FC4.HotelReservation.Domain.Entities;
using FC4.HotelReservation.Domain.ValueObjects;

namespace FC4.HotelReservation.Domain.Services.Interfaces;

public interface IRateService
{
    Task<Money> CalculateTotalAmountAsync(
        Guid hotelId,
        Guid roomTypeId,
        DateRange stayPeriod,
        int roomQuantity,
        CancellationToken cancellationToken);
}