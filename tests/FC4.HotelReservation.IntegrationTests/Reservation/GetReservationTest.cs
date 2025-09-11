using System.Net;
using System.Net.Http.Json;
using FC4.HotelReservation.Reservations.Application.UseCases.Reservation.Common;
using FC4.HotelReservation.Reservations.Domain.Enums;
using FluentAssertions;

namespace FC4.HotelReservation.IntegrationTests.Reservation;

[Collection(nameof(WebApiFixture))]
public class GetReservationTest(WebApiFixture fixture) : IAsyncDisposable
{
    private readonly HttpClient _client = fixture.CreateClient();
    
    [Fact]
    public async Task GetReservationById_WithValidId_ShouldReturnReservation()
    {
        // Arrange
        var reservation = await fixture.CreateReservationInDatabaseAsync();

        // Act
        var response = await _client.GetAsync($"/v1/reservations/{reservation.Id}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var output = await response.Content.ReadFromJsonAsync<ReservationOutput>(fixture.JsonSettings);
        
        output.Should().NotBeNull();
        output.Id.Should().Be(reservation.Id);
        output.HotelId.Should().Be(reservation.HotelId);
        output.RoomTypeId.Should().Be(reservation.RoomTypeId);
        output.StartDate.Date.Should().Be(reservation.StayPeriod.StartDate.Date);
        output.EndDate.Date.Should().Be(reservation.StayPeriod.EndDate.Date);
        output.Status.Should().Be(ReservationStatus.Pending);
        output.RoomQuantity.Should().Be(reservation.RoomQuantity);
        output.CreatedAt.Should().BeCloseTo(reservation.CreatedAt, TimeSpan.FromSeconds(1));
    }
    
    [Fact]
    public async Task GetReservationById_WithInvalidId_ShouldReturnNotFound()
    {
        // Arrange
        var nonExistentId = Guid.NewGuid();

        // Act
        var response = await _client.GetAsync($"/v1/reservations/{nonExistentId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    public async ValueTask DisposeAsync()
    {
        await fixture.CleanDatabaseAsync();
    }
}