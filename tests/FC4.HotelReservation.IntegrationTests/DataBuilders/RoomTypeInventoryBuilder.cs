using Bogus;
using FC4.HotelReservation.Reservations.Domain.Entities;

namespace FC4.HotelReservation.IntegrationTests.DataBuilders;

public class RoomTypeInventoryBuilder
{
    private readonly Faker _faker = new();
    private Guid _hotelId = Guid.NewGuid();
    private Guid _roomTypeId = Guid.NewGuid();
    private DateTime _date;
    private int _totalInventory;
    private int _totalReserved;

    public RoomTypeInventoryBuilder()
    {
        _date = _faker.Date.Future();
        _totalInventory = _faker.Random.Int(1, 100);
    }

    public static RoomTypeInventoryBuilder ARoomTypeInventory() => new();

    public RoomTypeInventoryBuilder WithHotelId(Guid hotelId)
    {
        _hotelId = hotelId;
        return this;
    }

    public RoomTypeInventoryBuilder WithRoomTypeId(Guid roomTypeId)
    {
        _roomTypeId = roomTypeId;
        return this;
    }

    public RoomTypeInventoryBuilder WithDate(DateTime date)
    {
        _date = date;
        return this;
    }

    public RoomTypeInventoryBuilder WithTotalInventory(int totalInventory)
    {
        _totalInventory = totalInventory;
        return this;
    }

    public RoomTypeInventoryBuilder WithTotalReserved(int totalReserved)
    {
        _totalReserved = totalReserved;
        return this;
    }

    public RoomTypeInventory Build()
    {
        return new RoomTypeInventory(Guid.NewGuid(), _hotelId, _roomTypeId, _date, _totalInventory, _totalReserved);
    }
}