using FC4.HotelReservation.Reservations.Domain.ValueObjects;

namespace FC4.HotelReservation.Reservations.Application.Gateway;

public interface ICatalogRateGateway
{
    Task<Money> CalculateTotalAmountAsync(
        Guid hotelId,
        Guid roomTypeId,
        DateRange stayPeriod,
        int roomQuantity,
        CancellationToken cancellationToken);
}