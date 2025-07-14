using Ardalis.GuardClauses;
using FC4.HotelReservation.Domain.Common;
using FC4.HotelReservation.Domain.Enums;
using FC4.HotelReservation.Domain.Events;
using FC4.HotelReservation.Domain.ValueObjects;

namespace FC4.HotelReservation.Domain.Entities;

public class Reservation : AggregateRoot
{
    public Guid HotelId { get; private set; }
    public Guid RoomTypeId { get; private set; }
    public DateRange StayPeriod { get; private set; }
    public ReservationStatus Status { get; private set; }
    public Guid GuestId { get; private set; }
    public int RoomQuantity { get; private set; }
    public Money TotalAmount { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Reservation() { } // For EF Core
    
    private Reservation(
        Guid hotelId,
        Guid roomTypeId,
        DateRange stayPeriod,
        Guid guestId,
        int roomQuantity,
        Money totalAmount)
    {
        HotelId = Guard.Against.Default(hotelId, nameof(hotelId));
        RoomTypeId = Guard.Against.Default(roomTypeId, nameof(roomTypeId));
        StayPeriod = Guard.Against.Null(stayPeriod, nameof(stayPeriod));
        GuestId = Guard.Against.Default(guestId, nameof(guestId));
        RoomQuantity = Guard.Against.NegativeOrZero(roomQuantity, nameof(roomQuantity));
        TotalAmount = Guard.Against.Null(totalAmount, nameof(totalAmount));
        Status = ReservationStatus.Pending;
        CreatedAt = DateTime.UtcNow;
        RaiseEvent(new ReservationCreatedEvent(Id, TotalAmount));
    }
    
    public static Reservation Create(
        Guid hotelId,
        Guid roomTypeId,
        DateRange stayPeriod,
        Guid guestId,
        int roomQuantity,
        Money totalAmount)
    {
        return new Reservation(hotelId, roomTypeId, stayPeriod, guestId, roomQuantity, totalAmount);
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