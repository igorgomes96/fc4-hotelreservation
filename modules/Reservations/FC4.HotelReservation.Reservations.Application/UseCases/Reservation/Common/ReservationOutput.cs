using FC4.HotelReservation.Reservations.Domain.Enums;

namespace FC4.HotelReservation.Reservations.Application.UseCases.Reservation.Common;

public record ReservationOutput(
    Guid Id,
    Guid HotelId,
    Guid RoomTypeId,
    DateTime StartDate,
    DateTime EndDate,
    ReservationStatus Status,
    int RoomQuantity,
    decimal Amount,
    string Currency,
    DateTime CreatedAt)
{
    public static ReservationOutput FromReservation(Domain.Entities.Reservation reservation)
    {
        return new ReservationOutput(
            reservation.Id,
            reservation.HotelId,
            reservation.RoomTypeId,
            reservation.StayPeriod.StartDate,
            reservation.StayPeriod.EndDate,
            reservation.Status,
            reservation.RoomQuantity,
            reservation.TotalAmount.Value,
            reservation.TotalAmount.Currency,
            reservation.CreatedAt);
    }
}