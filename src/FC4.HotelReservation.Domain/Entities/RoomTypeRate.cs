using Ardalis.GuardClauses;
using FC4.HotelReservation.Domain.Common;
using FC4.HotelReservation.Domain.ValueObjects;

namespace FC4.HotelReservation.Domain.Entities;

public class RoomTypeRate : AggregateRoot
{
    public Guid HotelId { get; private set; }
    public Guid RoomTypeId { get; private set; }
    public DateTime Date { get; private set; }
    public Money Rate { get; private set; }


    public RoomTypeRate(Guid hotelId, Guid roomTypeId, DateTime date, Money rate)
    {
        HotelId = Guard.Against.Default(hotelId, nameof(hotelId));
        RoomTypeId = Guard.Against.Default(roomTypeId, nameof(roomTypeId));
        Date = Guard.Against.Default(date, nameof(date));
        Rate = Guard.Against.Null(rate, nameof(rate));
    }

    public void UpdateRate(Money newRate)
    {
        Rate = Guard.Against.Null(newRate, nameof(newRate));
    }
}