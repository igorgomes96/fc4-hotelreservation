using FC4.HotelReservation.Domain.Entities;
using FC4.HotelReservation.Domain.ValueObjects;

namespace FC4.HotelReservation.Domain.Services.Interfaces;

public interface IRateService
{
    Money CalculateTotalAmountAsync(DateRange stayPeriod, int roomQuantity, IEnumerable<RoomTypeRate> rates);
}