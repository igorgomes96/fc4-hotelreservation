using System.Net;
using System.Net.Http.Json;
using FC4.HotelReservation.Reservations.Application.UseCases.Reservation.Common;
using FluentAssertions;
using static FC4.HotelReservation.IntegrationTests.DataBuilders.ReservationBuilder;

namespace FC4.HotelReservation.IntegrationTests.Reservation;

[Collection(nameof(WebApiFixture))]
public class ListReservationsTest(WebApiFixture fixture) : IAsyncDisposable
{
    private readonly HttpClient _client = fixture.CreateClient();
    
    [Fact]
    public async Task GetReservationsByGuest_WithValidGuestId_ShouldReturnReservations()
    {
        // Arrange
        var hotel = await fixture.CreateHotelInDatabaseAsync();
        var roomType = await fixture.CreateRoomTypeInDatabaseAsync();
        var guest = await fixture.CreateGuestInDatabaseAsync();
        var otherGuest = await fixture.CreateGuestInDatabaseAsync();
        
        // Create reservations for the guest
        var reservation1 = await fixture.CreateReservationInDatabaseAsync(
            AReservation()
                .WithGuestId(guest.Id)
                .WithHotelId(hotel.Id)
                .WithRoomTypeId(roomType.Id)
                .Build());
        var reservation2 = await fixture.CreateReservationInDatabaseAsync(
            AReservation()
                .WithGuestId(guest.Id)
                .WithHotelId(hotel.Id)
                .WithRoomTypeId(roomType.Id)
                .Build());
        
        // Create reservation for other guest (should not be returned)
        await fixture.CreateReservationInDatabaseAsync(
            AReservation()
                .WithGuestId(otherGuest.Id)
                .WithHotelId(hotel.Id)
                .WithRoomTypeId(roomType.Id)
                .Build());

        // Act
        var response = await _client.GetAsync($"/v1/reservations?guestId={guest.Id}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var reservations = await response.Content.ReadFromJsonAsync<List<ReservationOutput>>(fixture.JsonSettings);
        
        reservations.Should().NotBeNull();
        reservations.Should().HaveCount(2);
        
        var reservation1Output = reservations.First(r => r.Id == reservation1.Id);
        reservation1Output.HotelId.Should().Be(reservation1.HotelId);
        reservation1Output.RoomTypeId.Should().Be(reservation1.RoomTypeId);
        reservation1Output.StartDate.Date.Should().Be(reservation1.StayPeriod.StartDate.Date);
        reservation1Output.EndDate.Date.Should().Be(reservation1.StayPeriod.EndDate.Date);
        reservation1Output.Status.Should().Be(reservation1.Status);
        reservation1Output.RoomQuantity.Should().Be(reservation1.RoomQuantity);
        reservation1Output.CreatedAt.Should().BeCloseTo(reservation1.CreatedAt, TimeSpan.FromSeconds(1));
        
        var reservation2Output = reservations.First(r => r.Id == reservation2.Id);
        reservation2Output.HotelId.Should().Be(reservation2.HotelId);
        reservation2Output.RoomTypeId.Should().Be(reservation2.RoomTypeId);
        reservation2Output.StartDate.Date.Should().Be(reservation2.StayPeriod.StartDate.Date);
        reservation2Output.EndDate.Date.Should().Be(reservation2.StayPeriod.EndDate.Date);
        reservation2Output.Status.Should().Be(reservation2.Status);
        reservation2Output.RoomQuantity.Should().Be(reservation2.RoomQuantity);
        reservation2Output.CreatedAt.Should().BeCloseTo(reservation2.CreatedAt, TimeSpan.FromSeconds(1));
    }
    
    public async ValueTask DisposeAsync()
    {
        await fixture.CleanDatabaseAsync();
    }
}