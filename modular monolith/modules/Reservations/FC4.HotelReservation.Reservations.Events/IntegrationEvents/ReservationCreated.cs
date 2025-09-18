using FC4.HotelReservation.Shared.Application.IntegrationEvents;

namespace FC4.HotelReservation.Reservations.Events.IntegrationEvents;

public class ReservationCreated(Guid reservationId, decimal amount, string currency) : IntegrationEvent
{
    public Guid ReservationId { get; } = reservationId;
    public decimal Amount { get; } = amount;
    public string Currency { get; } = currency;
}
