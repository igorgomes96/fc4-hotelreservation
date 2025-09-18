using System.Net;
using System.Net.Http.Json;
using FC4.HotelReservation.Catalog.Application.UseCases.Room.GetRoom;
using FluentAssertions;
using static FC4.HotelReservation.IntegrationTests.DataBuilders.RoomBuilder;

namespace FC4.HotelReservation.IntegrationTests.Room;

[Collection(nameof(WebApiFixture))]
public class GetRoomByIdTest(WebApiFixture fixture) : IAsyncDisposable
{
    private readonly HttpClient _client = fixture.CreateClient();

    [Fact]
    public async Task GetRoomById_WithExistingRoom_ShouldReturnRoomData()
    {
        // Arrange
        var room = await fixture.CreateRoomInDatabaseAsync();

        // Act
        var response = await _client.GetAsync($"/v1/rooms/{room.Id}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var output = await response.Content.ReadFromJsonAsync<GetRoomOutput>(fixture.JsonSettings);

        output.Should().BeEquivalentTo(room, opt => opt.Excluding(r => r.Events));
    }

    [Fact]
    public async Task GetRoomById_WithNonExistentRoom_ShouldReturnNotFound()
    {
        // Arrange
        var nonExistentId = Guid.NewGuid();

        // Act
        var response = await _client.GetAsync($"/v1/rooms/{nonExistentId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }


    public async ValueTask DisposeAsync()
    {
        await fixture.CleanDatabaseAsync();
    }
}