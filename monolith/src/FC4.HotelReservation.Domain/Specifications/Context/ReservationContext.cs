using FC4.HotelReservation.Domain.ValueObjects;

namespace FC4.HotelReservation.Domain.Specifications.Context;

public record ReservationContext(
    DateRange StayPeriod,
    int RoomQuantity,
    DateTime CreatedAt);