using System.Net;
using System.Net.Http.Json;
using FC4.HotelReservation.Catalog.Application.UseCases.Hotel.GetHotel;
using FluentAssertions;

namespace FC4.HotelReservation.IntegrationTests.Hotel;

[Collection(nameof(WebApiFixture))]
public class GetHotelTest(WebApiFixture fixture) : IAsyncDisposable
{
    private readonly HttpClient _client = fixture.CreateClient();
    
    [Fact]
    public async Task GetHotelById_WithExistingHotel_ShouldReturnHotelData()
    {
        // Arrange
        var hotel = await fixture.CreateHotelInDatabaseAsync();
    
        // Act
        var response = await _client.GetAsync($"/v1/hotels/{hotel.Id}");
    
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var output = await response.Content.ReadFromJsonAsync<GetHotelOutput>(fixture.JsonSettings);
    
        output.Should().NotBeNull();
        output.Id.Should().Be(hotel.Id);
        output.Name.Should().Be(hotel.Name);
        output.Street.Should().Be(hotel.Address.Street);
        output.City.Should().Be(hotel.Address.City);
        output.State.Should().Be(hotel.Address.State);
        output.Country.Should().Be(hotel.Address.Country);
        output.ZipCode.Should().Be(hotel.Address.ZipCode);
    }
    
    [Fact]
    public async Task GetHotelById_WithNonExistentHotel_ShouldReturnNotFound()
    {
        // Arrange
        var nonExistentId = Guid.NewGuid();
        
        // Act
        var response = await _client.GetAsync($"/v1/hotels/{nonExistentId}");
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    public async ValueTask DisposeAsync()
    {
        await fixture.CleanDatabaseAsync();
    }
}
