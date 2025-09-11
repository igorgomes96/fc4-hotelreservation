using Bogus;
using FC4.HotelReservation.Reservations.Domain.Enums;
using FC4.HotelReservation.Reservations.Domain.ValueObjects;

namespace FC4.HotelReservation.IntegrationTests.DataBuilders;

public class ReservationBuilder
{
    private readonly Faker _faker = new();
    private Guid _id = Guid.NewGuid();
    private Guid _hotelId = Guid.NewGuid();
    private Guid _roomTypeId = Guid.NewGuid();
    private Guid _guestId = Guid.NewGuid();
    private DateTime _checkInDate;
    private DateTime _checkOutDate;
    private ReservationStatus _status = ReservationStatus.Pending;
    private int _roomQuantity;
    private readonly decimal _totalAmount;

    public ReservationBuilder()
    {
        _checkInDate = _faker.Date.Future();
        _checkOutDate = _checkInDate.AddDays(_faker.Random.Int(1, 14));
        _totalAmount = _faker.Random.Decimal(100, 5000);
        _roomQuantity = _faker.Random.Int(1, 5);
    }

    public static ReservationBuilder AReservation() => new();

    public ReservationBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public ReservationBuilder WithHotelId(Guid hotelId)
    {
        _hotelId = hotelId;
        return this;
    }

    public ReservationBuilder WithRoomTypeId(Guid roomTypeId)
    {
        _roomTypeId = roomTypeId;
        return this;
    }

    public ReservationBuilder WithGuestId(Guid guestId)
    {
        _guestId = guestId;
        return this;
    }

    public ReservationBuilder WithStartDate(DateTime startDate)
    {
        _checkInDate = startDate;
        return this;
    }

    public ReservationBuilder WithEndDate(DateTime endDate)
    {
        _checkOutDate = endDate;
        return this;
    }

    public ReservationBuilder WithStatus(ReservationStatus status)
    {
        _status = status;
        return this;
    }

    public ReservationBuilder WithRoomQuantity(int roomQuantity)
    {
        _roomQuantity = roomQuantity;
        return this;
    }

    public Reservations.Domain.Entities.Reservation Build()
    {
        return new Reservations.Domain.Entities.Reservation(_id, _hotelId, _roomTypeId,
            new DateRange(_checkInDate, _checkOutDate), _guestId, _roomQuantity,
            new Money(_totalAmount, "BRL"), _status, DateTime.UtcNow);
    }
}