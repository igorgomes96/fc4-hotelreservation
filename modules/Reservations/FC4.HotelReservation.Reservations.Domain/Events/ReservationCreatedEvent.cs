using FC4.HotelReservation.Shared.Domain;
using FC4.HotelReservation.Shared.Domain.ValueObjects;

namespace FC4.HotelReservation.Reservations.Domain.Events;

public class ReservationCreatedEvent(Guid reservationId, Money amount) : DomainEvent
{
    public Guid ReservationId { get; } = reservationId;
    public Money Amount { get; } = amount;
}