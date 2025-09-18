using Bogus;
using FC4.HotelReservation.Catalog.Application.UseCases.Room.CreateRoom;

namespace FC4.HotelReservation.IntegrationTests.DataBuilders;

public class CreateRoomInputBuilder
{
    private readonly Faker _faker = new();
    private Guid _roomTypeId = Guid.NewGuid();
    private int _floor;
    private string _number;
    private Guid _hotelId = Guid.NewGuid();
    private bool _isAvailable = true;

    public CreateRoomInputBuilder()
    {
        _floor = _faker.Random.Int(1, 20);
        _number = _faker.Random.Int(100, 999).ToString();
    }

    public static CreateRoomInputBuilder ACreateRoomInput() => new();

    public CreateRoomInputBuilder WithRoomTypeId(Guid roomTypeId)
    {
        _roomTypeId = roomTypeId;
        return this;
    }

    public CreateRoomInputBuilder WithFloor(int floor)
    {
        _floor = floor;
        return this;
    }

    public CreateRoomInputBuilder WithNumber(string number)
    {
        _number = number;
        return this;
    }

    public CreateRoomInputBuilder WithHotelId(Guid hotelId)
    {
        _hotelId = hotelId;
        return this;
    }

    public CreateRoomInputBuilder WithIsAvailable(bool isAvailable)
    {
        _isAvailable = isAvailable;
        return this;
    }

    public CreateRoomInput Build() => new(
        RoomTypeId: _roomTypeId,
        Floor: _floor,
        Number: _number,
        HotelId: _hotelId,
        IsAvailable: _isAvailable
    );
}