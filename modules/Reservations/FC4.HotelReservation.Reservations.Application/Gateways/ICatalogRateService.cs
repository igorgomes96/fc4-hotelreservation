using FC4.HotelReservation.Reservations.Domain.ValueObjects;

namespace FC4.HotelReservation.Reservations.Application.Gateways;

public interface ICatalogRateService
{
    Task<Money> CalculateTotalAmountAsync(
        Guid hotelId,
        Guid roomTypeId,
        DateRange stayPeriod,
        int roomQuantity,
        CancellationToken cancellationToken);
}