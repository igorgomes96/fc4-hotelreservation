using System.Net;
using System.Net.Http.Json;
using FC4.HotelReservation.Catalog.Application.UseCases.Hotel.CreateHotel;
using FluentAssertions;
using static FC4.HotelReservation.IntegrationTests.DataBuilders.CreateHotelInputBuilder;

namespace FC4.HotelReservation.IntegrationTests.Hotel;

[Collection(nameof(WebApiFixture))]
public class CreateHotelTest(WebApiFixture fixture) : IAsyncDisposable
{
    private readonly HttpClient _client = fixture.CreateClient();
    
    [Fact]
    public async Task CreateHotel_WithValidData_ShouldReturnCreatedAndSaveInDatabase()
    {
        var input = ACreateHotelInput();

        var response = await _client.PostAsJsonAsync("/v1/hotels", input);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var output = await response.Content.ReadFromJsonAsync<CreateHotelOutput>(fixture.JsonSettings);
        output.Should().NotBeNull();
        output.Id.Should().NotBeEmpty();
        
        var savedHotel = await fixture.GetHotelByIdAsync(output.Id);
        savedHotel.Should().NotBeNull();
        savedHotel.Name.Should().Be(input.Name);
        savedHotel.Address.Street.Should().Be(input.Street);
        savedHotel.Address.City.Should().Be(input.City);
        savedHotel.Address.State.Should().Be(input.State);
        savedHotel.Address.Country.Should().Be(input.Country);
        savedHotel.Address.ZipCode.Should().Be(input.ZipCode);
    }

    public async ValueTask DisposeAsync()
    {
        await fixture.CleanDatabaseAsync();
    }
}