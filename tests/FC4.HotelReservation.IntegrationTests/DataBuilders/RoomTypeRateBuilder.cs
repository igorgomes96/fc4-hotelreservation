using Bogus;
using FC4.HotelReservation.Catalog.Domain.Entities;
using FC4.HotelReservation.Catalog.Domain.ValueObjects;

namespace FC4.HotelReservation.IntegrationTests.DataBuilders;

public class RoomTypeRateBuilder
{
    private readonly Faker _faker = new();
    private Guid _hotelId = Guid.NewGuid();
    private Guid _roomTypeId = Guid.NewGuid();
    private DateTime _date = DateTime.Today;
    private Money _rate;

    public RoomTypeRateBuilder()
    {
        _rate = new Money(_faker.Random.Decimal(100, 500), "USD");
    }
    
    public static RoomTypeRateBuilder ARoomTypeRate() => new();

    public RoomTypeRateBuilder WithHotelId(Guid hotelId)
    {
        _hotelId = hotelId;
        return this;
    }

    public RoomTypeRateBuilder WithRoomTypeId(Guid roomTypeId)
    {
        _roomTypeId = roomTypeId;
        return this;
    }

    public RoomTypeRateBuilder WithDate(DateTime date)
    {
        _date = date;
        return this;
    }

    public RoomTypeRateBuilder WithRate(Money rate)
    {
        _rate = rate;
        return this;
    }

    public RoomTypeRate Build() => new(Guid.NewGuid(), _hotelId, _roomTypeId, _date, _rate);
}