using Ardalis.GuardClauses;
using FC4.HotelReservation.Reservations.Domain.Enums;
using FC4.HotelReservation.Reservations.Domain.Events;
using FC4.HotelReservation.Reservations.Domain.ValueObjects;
using FC4.HotelReservation.Shared.Domain;

namespace FC4.HotelReservation.Reservations.Domain.Entities;

public class Reservation : AggregateRoot
{
    public Guid HotelId { get; }
    public Guid RoomTypeId { get; }
    public DateRange StayPeriod { get; }
    public ReservationStatus Status { get; private set; }
    public Guid GuestId { get; private set; }
    public int RoomQuantity { get; }
    public Money TotalAmount { get; }
    public DateTime CreatedAt { get; private set; }

    private Reservation()
    {
    } // For EF Core

    public Reservation(
        Guid id,
        Guid hotelId,
        Guid roomTypeId,
        DateRange stayPeriod,
        Guid guestId,
        int roomQuantity,
        Money totalAmount,
        ReservationStatus status,
        DateTime createdAt) : base(id)
    {
        HotelId = Guard.Against.Default(hotelId, nameof(hotelId));
        RoomTypeId = Guard.Against.Default(roomTypeId, nameof(roomTypeId));
        StayPeriod = Guard.Against.Null(stayPeriod, nameof(stayPeriod));
        GuestId = Guard.Against.Default(guestId, nameof(guestId));
        RoomQuantity = Guard.Against.NegativeOrZero(roomQuantity, nameof(roomQuantity));
        TotalAmount = Guard.Against.Null(totalAmount, nameof(totalAmount));
        Status = Guard.Against.EnumOutOfRange(status, nameof(status));
        CreatedAt = Guard.Against.Default(createdAt, nameof(createdAt));
    }

    public static Reservation Create(
        Guid hotelId,
        Guid roomTypeId,
        DateRange stayPeriod,
        Guid guestId,
        int roomQuantity,
        Money totalAmount)
    {
        var reservation = new Reservation(Guid.NewGuid(), hotelId, roomTypeId, stayPeriod, guestId,
            roomQuantity, totalAmount, ReservationStatus.Pending, DateTime.UtcNow);
        reservation.RaiseEvent(new ReservationCreatedEvent(reservation.Id, reservation.TotalAmount));
        return reservation;
    }

    public void Cancel()
    {
        if (Status is ReservationStatus.Cancelled or ReservationStatus.Rejected)
            throw new InvalidOperationException("Reservation is already cancelled or rejected");

        Status = ReservationStatus.Cancelled;
        RaiseEvent(new ReservationCanceledEvent(Id, HotelId, RoomTypeId, StayPeriod, RoomQuantity));
    }

    public void Reject()
    {
        if (Status != ReservationStatus.Pending)
            throw new InvalidOperationException("Can only reject pending reservations");

        Status = ReservationStatus.Rejected;
        RaiseEvent(new ReservationCanceledEvent(Id, HotelId, RoomTypeId, StayPeriod, RoomQuantity));
    }

    public void MarkAsPaid()
    {
        if (Status is ReservationStatus.Cancelled or ReservationStatus.Rejected)
            throw new InvalidOperationException("Cannot mark cancelled or rejected reservation as paid");

        Status = ReservationStatus.Paid;
    }

    public void Refund()
    {
        if (Status != ReservationStatus.Paid)
            throw new InvalidOperationException("Can only refund paid reservations");

        Status = ReservationStatus.Refunded;
    }
}