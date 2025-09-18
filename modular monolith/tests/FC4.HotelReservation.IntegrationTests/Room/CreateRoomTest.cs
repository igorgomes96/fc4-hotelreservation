using System.Net;
using System.Net.Http.Json;
using FC4.HotelReservation.Catalog.Application.UseCases.Room.CreateRoom;
using FluentAssertions;
using static FC4.HotelReservation.IntegrationTests.DataBuilders.CreateRoomInputBuilder;

namespace FC4.HotelReservation.IntegrationTests.Room;

[Collection(nameof(WebApiFixture))]
public class CreateRoomTest(WebApiFixture fixture) : IAsyncDisposable
{
    private readonly HttpClient _client = fixture.CreateClient();

    [Fact]
    public async Task CreateRoom_WithValidData_ShouldReturnCreatedAndSaveInDatabase()
    {
        // Arrange
        var roomType = await fixture.CreateRoomTypeInDatabaseAsync();
        var hotel = await fixture.CreateHotelInDatabaseAsync();
        var input = ACreateRoomInput()
            .WithRoomTypeId(roomType.Id)
            .WithHotelId(hotel.Id)
            .Build();

        // Act
        var response = await _client.PostAsJsonAsync("/v1/rooms", input);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var output = await response.Content.ReadFromJsonAsync<CreateRoomOutput>(fixture.JsonSettings);
        output.Should().NotBeNull();
        output.Id.Should().NotBeEmpty();

        var savedRoom = await fixture.GetRoomByIdAsync(output.Id);
        savedRoom.Should().BeEquivalentTo(input, opt => opt.ExcludingMissingMembers());
    }

    [Fact]
    public async Task CreateRoom_WithNonExistingType_ShouldReturnNotFound()
    {
        // Arrange
        var hotel = await fixture.CreateHotelInDatabaseAsync();
        var input = ACreateRoomInput()
            .WithRoomTypeId(Guid.NewGuid())
            .WithHotelId(hotel.Id)
            .Build();

        // Act
        var response = await _client.PostAsJsonAsync("/v1/rooms", input);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    [Fact]
    public async Task CreateRoom_WithNonExistingHotel_ShouldReturnNotFound()
    {
        // Arrange
        var roomType = await fixture.CreateRoomTypeInDatabaseAsync();
        var input = ACreateRoomInput()
            .WithRoomTypeId(roomType.Id)
            .WithHotelId(Guid.NewGuid())
            .Build();

        // Act
        var response = await _client.PostAsJsonAsync("/v1/rooms", input);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    public async ValueTask DisposeAsync()
    {
        await fixture.CleanDatabaseAsync();
    }
}