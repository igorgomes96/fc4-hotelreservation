using Ardalis.GuardClauses;
using FC4.HotelReservation.Catalog.Domain.ValueObjects;
using FC4.HotelReservation.Shared.Domain;

namespace FC4.HotelReservation.Catalog.Domain.Entities;

public class RoomTypeRate : AggregateRoot
{
    public Guid HotelId { get; private set; }
    public Guid RoomTypeId { get; private set; }
    public DateTime Date { get; private set; }
    public Money Rate { get; private set; }

    private RoomTypeRate() { } // For EF Core
    
    public RoomTypeRate(Guid id, Guid hotelId, Guid roomTypeId, DateTime date, Money rate) : base(id)
    {
        HotelId = Guard.Against.Default(hotelId, nameof(hotelId));
        RoomTypeId = Guard.Against.Default(roomTypeId, nameof(roomTypeId));
        Date = Guard.Against.Default(date, nameof(date));
        Rate = Guard.Against.Null(rate, nameof(rate));
    }
    
    public static RoomTypeRate Create(Guid hotelId, Guid roomTypeId, DateTime date, Money rate)
    {
        return new RoomTypeRate(Guid.NewGuid(), hotelId, roomTypeId, date, rate);
    }
}