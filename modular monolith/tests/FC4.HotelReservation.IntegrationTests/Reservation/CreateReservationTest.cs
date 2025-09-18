using System.Net;
using System.Net.Http.Json;
using FC4.HotelReservation.Payments.Domain.Enums;
using FC4.HotelReservation.Reservations.Application.UseCases.Reservation.CreateReservation;
using FC4.HotelReservation.Reservations.Domain.ValueObjects;
using FluentAssertions;
using static FC4.HotelReservation.IntegrationTests.DataBuilders.CreateReservationInputBuilder;
using static FC4.HotelReservation.IntegrationTests.DataBuilders.RoomTypeRateBuilder;
using static FC4.HotelReservation.IntegrationTests.DataBuilders.RoomTypeInventoryBuilder;

namespace FC4.HotelReservation.IntegrationTests.Reservation;

[Collection(nameof(WebApiFixture))]
public class CreateReservationTest(WebApiFixture fixture) : IAsyncDisposable
{
    private readonly HttpClient _client = fixture.CreateClient();

    [Fact]
    public async Task CreateReservation_WithValidData_ShouldCreateReservationUpdateInventoryAndCreatePayment()
    {
        // Arrange
        var hotel = await fixture.CreateHotelInDatabaseAsync();
        var guest = await fixture.CreateGuestInDatabaseAsync();
        var roomType = await fixture.CreateRoomTypeInDatabaseAsync();
        var startDate = DateTime.Today.AddDays(1);
        var endDate = startDate.AddDays(3);
        var dailyRate = new Catalog.Domain.ValueObjects.Money(150.00m, "USD");
        const int roomQuantity = 2;
        const int totalInventory = 5;
        const decimal lastMinuteRateIncrease = 1.15m;
        var expectedTotalAmount = dailyRate.Value * (endDate - startDate).Days * roomQuantity * lastMinuteRateIncrease;

        // Create inventory and rate for each day in the period
        var dates = new DateRange(startDate, endDate).GetDates().ToList();
        foreach (var date in dates)
        {
            await fixture.CreateRoomTypeInventoryInDatabaseAsync(
                ARoomTypeInventory()
                    .WithHotelId(hotel.Id)
                    .WithRoomTypeId(roomType.Id)
                    .WithDate(date)
                    .WithTotalInventory(totalInventory)
                    .Build());

            await fixture.CreateRoomTypeRateInDatabaseAsync(
                ARoomTypeRate()
                    .WithHotelId(hotel.Id)
                    .WithRoomTypeId(roomType.Id)
                    .WithDate(date)
                    .WithRate(dailyRate)
                    .Build());
        }

        var input = ACreateReservationInput()
            .WithHotelId(hotel.Id)
            .WithRoomTypeId(roomType.Id)
            .WithGuestId(guest.Id)
            .WithStartDate(startDate)
            .WithEndDate(endDate)
            .WithRoomQuantity(roomQuantity)
            .Build();

        // Act
        var response = await _client.PostAsJsonAsync("/v1/reservations", input);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var output = await response.Content.ReadFromJsonAsync<CreateReservationOutput>(fixture.JsonSettings);
        output.Should().NotBeNull();
        output.Id.Should().NotBeEmpty();

        // Verify reservation was created
        var savedReservation = await fixture.GetReservationByIdAsync(output.Id);
        savedReservation.Should().NotBeNull();
        savedReservation.HotelId.Should().Be(input.HotelId);
        savedReservation.RoomTypeId.Should().Be(input.RoomTypeId);
        savedReservation.GuestId.Should().Be(input.GuestId);
        savedReservation.StayPeriod.StartDate.Should().Be(input.StartDate);
        savedReservation.StayPeriod.EndDate.Should().Be(input.EndDate);
        savedReservation.RoomQuantity.Should().Be(input.RoomQuantity);
        savedReservation.TotalAmount.Value.Should().Be(expectedTotalAmount);

        // Verify inventory was updated
        var period = new DateRange(startDate, endDate);
        var updatedInventories = await fixture.GetRoomTypeInventoriesAsync(hotel.Id, roomType.Id, period);
        updatedInventories.Should().HaveCount(dates.Count);
        updatedInventories.Should().OnlyContain(i => i.TotalReserved == roomQuantity);
        updatedInventories.Should().OnlyContain(i => i.AvailableInventory == totalInventory - roomQuantity);

        // Verify payment was created via event
        await Task.Delay(1000);
        var payment = await fixture.GetPaymentByReservationIdAsync(output.Id);
        payment.Should().NotBeNull();
        payment.ReservationId.Should().Be(output.Id);
        payment.Status.Should().Be(PaymentStatus.Pending);
        payment.Amount.Value.Should().Be(expectedTotalAmount);
        payment.ProcessedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMinutes(1));
    }

    [Fact]
    public async Task CreateReservation_WithInsufficientInventory_ShouldReturnBadRequest()
    {
        // Arrange
        var hotel = await fixture.CreateHotelInDatabaseAsync();
        var guest = await fixture.CreateGuestInDatabaseAsync();
        var roomType = await fixture.CreateRoomTypeInDatabaseAsync();
        var startDate = DateTime.Today.AddDays(1);
        var endDate = startDate.AddDays(2);
        var roomQuantity = 5;

        // Create inventory with insufficient rooms
        await fixture.CreateRoomTypeInventoryInDatabaseAsync(
            ARoomTypeInventory()
                .WithHotelId(hotel.Id)
                .WithRoomTypeId(roomType.Id)
                .WithDate(startDate)
                .WithTotalInventory(3)
                .Build());

        var input = ACreateReservationInput()
            .WithHotelId(hotel.Id)
            .WithRoomTypeId(roomType.Id)
            .WithGuestId(guest.Id)
            .WithStartDate(startDate)
            .WithEndDate(endDate)
            .WithRoomQuantity(roomQuantity)
            .Build();

        // Act
        var response = await _client.PostAsJsonAsync("/v1/reservations", input);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);

        // Verify no reservation was created
        var reservations = await fixture.GetReservationsByGuestIdAsync(guest.Id);
        reservations.Should().BeEmpty();

        // Verify inventory was not modified
        var inventory = await fixture.GetRoomTypeInventoryAsync(hotel.Id, roomType.Id, startDate);
        inventory.Should().NotBeNull();
        inventory.TotalReserved.Should().Be(0);
    }

    public async ValueTask DisposeAsync()
    {
        await fixture.CleanDatabaseAsync();
    }
}