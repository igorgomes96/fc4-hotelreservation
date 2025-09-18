using Bogus;
using FC4.HotelReservation.Reservations.Application.UseCases.Reservation.CreateReservation;

namespace FC4.HotelReservation.IntegrationTests.DataBuilders;

public class CreateReservationInputBuilder
{
    private readonly Faker _faker = new();
    private Guid _hotelId = Guid.NewGuid();
    private Guid _roomTypeId = Guid.NewGuid();
    private DateTime _startDate = DateTime.Today.AddDays(1);
    private DateTime _endDate = DateTime.Today.AddDays(5);
    private Guid _guestId = Guid.NewGuid();
    private int _roomQuantity = 1;

    public static CreateReservationInputBuilder ACreateReservationInput() => new();

    public CreateReservationInputBuilder WithHotelId(Guid hotelId)
    {
        _hotelId = hotelId;
        return this;
    }

    public CreateReservationInputBuilder WithRoomTypeId(Guid roomTypeId)
    {
        _roomTypeId = roomTypeId;
        return this;
    }

    public CreateReservationInputBuilder WithStartDate(DateTime startDate)
    {
        _startDate = startDate;
        return this;
    }

    public CreateReservationInputBuilder WithEndDate(DateTime endDate)
    {
        _endDate = endDate;
        return this;
    }

    public CreateReservationInputBuilder WithGuestId(Guid guestId)
    {
        _guestId = guestId;
        return this;
    }

    public CreateReservationInputBuilder WithRoomQuantity(int roomQuantity)
    {
        _roomQuantity = roomQuantity;
        return this;
    }

    public CreateReservationInput Build()
    {
        return new CreateReservationInput(_hotelId, _roomTypeId, _startDate, _endDate, _guestId, _roomQuantity);
    }
}