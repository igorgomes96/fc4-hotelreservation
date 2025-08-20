using FC4.HotelReservation.Shared.Domain;
using FC4.HotelReservation.Shared.Domain.ValueObjects;

namespace FC4.HotelReservation.Domain.Events;

public class ReservationCanceledEvent(
    Guid reservationId,
    Guid hotelId,
    Guid roomTypeId,
    DateRange stayPeriod,
    int roomQuantity) : DomainEvent
{
    public Guid ReservationId { get; } = reservationId;
    public Guid HotelId { get; } = hotelId;
    public Guid RoomTypeId { get; } = roomTypeId;
    public DateRange StayPeriod { get; } = stayPeriod;
    public int RoomQuantity { get; } = roomQuantity;
}