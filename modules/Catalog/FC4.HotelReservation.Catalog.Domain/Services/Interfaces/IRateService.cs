using FC4.HotelReservation.Shared.Domain.ValueObjects;

namespace FC4.HotelReservation.Catalog.Domain.Services.Interfaces;

public interface IRateService
{
    Task<Money> CalculateTotalAmountAsync(
        Guid hotelId,
        Guid roomTypeId,
        DateRange stayPeriod,
        int roomQuantity,
        CancellationToken cancellationToken);
}