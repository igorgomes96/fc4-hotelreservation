using System.Net;
using FC4.HotelReservation.Reservations.Domain.Enums;
using FC4.HotelReservation.Reservations.Domain.ValueObjects;
using FluentAssertions;
using static FC4.HotelReservation.IntegrationTests.DataBuilders.ReservationBuilder;
using static FC4.HotelReservation.IntegrationTests.DataBuilders.RoomTypeInventoryBuilder;

namespace FC4.HotelReservation.IntegrationTests.Reservation;

[Collection(nameof(WebApiFixture))]
public class CancelReservationTest(WebApiFixture fixture) : IAsyncDisposable
{
    private readonly HttpClient _client = fixture.CreateClient();

    [Fact]
    public async Task CancelReservation_WithValidId_ShouldCancelReservationAndRestoreInventory()
    {
        // Arrange
        var hotel = await fixture.CreateHotelInDatabaseAsync();
        var guest = await fixture.CreateGuestInDatabaseAsync();
        var roomType = await fixture.CreateRoomTypeInDatabaseAsync();
        var startDate = DateTime.Today.AddDays(1);
        var endDate = startDate.AddDays(3);
        const int totalInventory = 10;
        const int roomQuantity = 2;
        const int totalReserved = 5;

        // Create inventory for the period with rooms already reserved
        var dates = new DateRange(startDate, endDate).GetDates().ToList();
        foreach (var date in dates)
        {
            await fixture.CreateRoomTypeInventoryInDatabaseAsync(
                ARoomTypeInventory()
                    .WithHotelId(hotel.Id)
                    .WithRoomTypeId(roomType.Id)
                    .WithDate(date)
                    .WithTotalInventory(totalInventory)
                    .WithTotalReserved(totalReserved)
                    .Build());
        }

        var reservation = await fixture.CreateReservationInDatabaseAsync(
            AReservation()
                .WithHotelId(hotel.Id)
                .WithGuestId(guest.Id)
                .WithRoomTypeId(roomType.Id)
                .WithStartDate(startDate)
                .WithEndDate(endDate)
                .WithRoomQuantity(roomQuantity)
                .WithStatus(ReservationStatus.Paid)
                .Build());

        // Act
        var response = await _client.DeleteAsync($"/v1/reservations/{reservation.Id}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        // Verify reservation was cancelled
        var cancelledReservation = await fixture.GetReservationByIdAsync(reservation.Id);
        cancelledReservation.Should().NotBeNull();
        cancelledReservation.Status.Should().Be(ReservationStatus.Cancelled);

        // Verify inventory was restored
        var period = new DateRange(startDate, endDate);
        var updatedInventories = await fixture.GetRoomTypeInventoriesAsync(hotel.Id, roomType.Id, period);
        updatedInventories.Should().HaveCount(dates.Count);
        updatedInventories.Should().OnlyContain(i => i.TotalReserved == totalReserved - roomQuantity);
        updatedInventories.Should()
            .OnlyContain(i => i.AvailableInventory == totalInventory - totalReserved + roomQuantity);
    }

    [Fact]
    public async Task CancelReservation_WithInvalidId_ShouldReturnNotFound()
    {
        // Arrange
        var nonExistentId = Guid.NewGuid();

        // Act
        var response = await _client.DeleteAsync($"/v1/reservations/{nonExistentId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    public async ValueTask DisposeAsync()
    {
        await fixture.CleanDatabaseAsync();
    }
}