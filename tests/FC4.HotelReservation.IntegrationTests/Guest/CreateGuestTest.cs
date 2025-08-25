using System.Net;
using System.Net.Http.Json;
using FC4.HotelReservation.Guests.Application.UseCases.Guest.CreateGuest;
using FluentAssertions;
using static FC4.HotelReservation.IntegrationTests.DataBuilders.CreateGuestInputBuilder;

namespace FC4.HotelReservation.IntegrationTests.Guest;

[Collection(nameof(WebApiFixture))]
public class CreateGuestTest(WebApiFixture fixture) : IAsyncDisposable
{
    private readonly HttpClient _client = fixture.CreateClient();
    
    [Fact]
    public async Task CreateGuest_WithValidData_ShouldReturnCreatedAndSaveInDatabase()
    {
        var input = ACreateGuestInput();

        var response = await _client.PostAsJsonAsync("/v1/guests", input);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var output = await response.Content.ReadFromJsonAsync<CreateGuestOutput>(fixture.JsonSettings);
        output.Should().NotBeNull();
        output.Id.Should().NotBeEmpty();
        
        var savedGuest = await fixture.GetGuestByIdAsync(output.Id);
        savedGuest.Should().NotBeNull();
        savedGuest.FirstName.Should().Be(input.FirstName);
        savedGuest.LastName.Should().Be(input.LastName);
        savedGuest.Email.Value.Should().Be(input.Email);
    }

    public async ValueTask DisposeAsync()
    {
        await fixture.CleanDatabaseAsync();
    }
}