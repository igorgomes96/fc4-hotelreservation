using FC4.HotelReservation.Catalog.Domain.ValueObjects;

namespace FC4.HotelReservation.Catalog.Domain.Specifications.Context;

public record ReservationContext(
    DateRange StayPeriod,
    int RoomQuantity,
    DateTime CreatedAt);