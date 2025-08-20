using Ardalis.GuardClauses;
using FC4.HotelReservation.Shared.Domain;

namespace FC4.HotelReservation.Catalog.Domain.Entities;

public class Room : AggregateRoot
{
    public Guid HotelId { get; private set; }
    public Guid RoomTypeId { get; private set; }
    public int Floor { get; private set; }
    public string Number { get; private set; }
    public bool IsAvailable { get; private set; }

    private Room()
    {
    } // For EF Core

    public Room(Guid id, Guid roomTypeId, int floor, string number, Guid hotelId, bool isAvailable = true) : base(id)
    {
        RoomTypeId = Guard.Against.Default(roomTypeId, nameof(roomTypeId));
        Floor = Guard.Against.NegativeOrZero(floor, nameof(floor));
        Number = Guard.Against.NullOrWhiteSpace(number, nameof(number));
        HotelId = Guard.Against.Default(hotelId, nameof(hotelId));
        IsAvailable = isAvailable;
    }

    public static Room Create(
        Guid roomTypeId,
        int floor,
        string number,
        Guid hotelId,
        bool isAvailable = true)
    {
        return new Room(Guid.NewGuid(), roomTypeId, floor, number, hotelId, isAvailable);
    }
}