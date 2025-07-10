using Bogus;

namespace FC4.HotelReservation.IntegrationTests.DataBuilders;

public class RoomBuilder
{
    private readonly Faker _faker = new();
    private Guid _id = Guid.NewGuid();
    private Guid _roomTypeId = Guid.NewGuid();
    private int _floor;
    private string _number;
    private Guid _hotelId = Guid.NewGuid();
    private string _name;
    private bool _isAvailable = true;

    public RoomBuilder()
    {
        _floor = _faker.Random.Int(1, 20);
        _number = _faker.Random.Int(100, 999).ToString();
        _name = _faker.Commerce.ProductName();
    }
    
    public static RoomBuilder ARoom() => new();

    public RoomBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public RoomBuilder WithRoomTypeId(Guid roomTypeId)
    {
        _roomTypeId = roomTypeId;
        return this;
    }

    public RoomBuilder WithFloor(int floor)
    {
        _floor = floor;
        return this;
    }

    public RoomBuilder WithNumber(string number)
    {
        _number = number;
        return this;
    }

    public RoomBuilder WithHotelId(Guid hotelId)
    {
        _hotelId = hotelId;
        return this;
    }

    public RoomBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public RoomBuilder WithIsAvailable(bool isAvailable)
    {
        _isAvailable = isAvailable;
        return this;
    }

    public Domain.Entities.Room Build()
        => new(_roomTypeId, _floor, _number, _hotelId, _name, _isAvailable)
        {
            Id = _id
        };
}