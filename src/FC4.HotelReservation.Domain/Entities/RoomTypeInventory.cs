using Ardalis.GuardClauses;
using FC4.HotelReservation.Domain.Common;

namespace FC4.HotelReservation.Domain.Entities;
 
public class RoomTypeInventory : AggregateRoot
{
    public Guid HotelId { get; private set; }
    public Guid RoomTypeId { get; private set; }
    public DateTime Date { get; private set; }
    public int TotalInventory { get; private set; }
    public int TotalReserved { get; private set; }

    private RoomTypeInventory() { } // For EF Core
    
    public RoomTypeInventory(Guid hotelId, Guid roomTypeId, DateTime date, int totalInventory)
    {
        HotelId = Guard.Against.Default(hotelId, nameof(hotelId));
        RoomTypeId = Guard.Against.Default(roomTypeId, nameof(roomTypeId));
        Date = Guard.Against.Default(date, nameof(date));
        TotalInventory = Guard.Against.Negative(totalInventory, nameof(totalInventory));
        TotalReserved = 0;
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

    public void UpdateInventory(int newTotalInventory)
    {
        Guard.Against.Negative(newTotalInventory, nameof(newTotalInventory));
        
        if (newTotalInventory < TotalReserved)
            throw new InvalidOperationException("Total inventory cannot be less than total reserved");

        TotalInventory = newTotalInventory;
    }
}