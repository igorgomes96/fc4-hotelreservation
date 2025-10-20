using Ardalis.GuardClauses;
using FC4.HotelReservation.Shared.Domain;

namespace FC4.HotelReservation.Reservations.Domain.Entities;

public class RoomTypeInventory : AggregateRoot
{
    public Guid HotelId { get; private set; }
    public Guid RoomTypeId { get; private set; }
    public DateTime Date { get; private set; }
    public int TotalInventory { get; private set; }
    public int TotalReserved { get; private set; }

    private RoomTypeInventory()
    {
    } // For EF Core

    public RoomTypeInventory(
        Guid id,
        Guid hotelId,
        Guid roomTypeId,
        DateTime date,
        int totalInventory,
        int totalReserved) : base(id)
    {
        HotelId = Guard.Against.Default(hotelId, nameof(hotelId));
        RoomTypeId = Guard.Against.Default(roomTypeId, nameof(roomTypeId));
        Date = Guard.Against.Default(date, nameof(date));
        TotalInventory = Guard.Against.Negative(totalInventory, nameof(totalInventory));
        TotalReserved = Guard.Against.Negative(totalReserved, nameof(totalReserved));
    }
    
    public static RoomTypeInventory Create(
        Guid hotelId,
        Guid roomTypeId,
        DateTime date,
        int totalInventory)
    {
        return new RoomTypeInventory(
            Guid.NewGuid(),
            hotelId,
            roomTypeId,
            date,
            totalInventory,
            0);
    }

    public int AvailableInventory => TotalInventory - TotalReserved;

    public bool CanReserve(int quantity)
    {
        return AvailableInventory >= quantity;
    }

    public void ReserveRooms(int quantity)
    {
        Guard.Against.NegativeOrZero(quantity, nameof(quantity));

        if (!CanReserve(quantity))
            throw new InvalidOperationException("Insufficient inventory available");

        TotalReserved += quantity;
    }

    public void ReleaseRooms(int quantity)
    {
        Guard.Against.NegativeOrZero(quantity, nameof(quantity));

        if (quantity > TotalReserved)
            throw new InvalidOperationException("Cannot release more rooms than reserved");

        TotalReserved -= quantity;
    }
}