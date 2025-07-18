using System.Net;
using System.Net.Http.Json;
using FC4.HotelReservation.Catalog.Application.UseCases.Rate.GetRate;
using FC4.HotelReservation.Catalog.Domain.ValueObjects;
using FluentAssertions;
using static FC4.HotelReservation.IntegrationTests.DataBuilders.RoomTypeRateBuilder;

namespace FC4.HotelReservation.IntegrationTests.Rate;

[Collection(nameof(WebApiFixture))]
public class GetRateTest(WebApiFixture fixture) : IAsyncDisposable
{
    private readonly HttpClient _client = fixture.CreateClient();

    [Fact]
    public async Task GetRate_WithValidData_ShouldReturnCorrectAmountAndCurrency()
    {
        // Arrange
        var hotel = await fixture.CreateHotelInDatabaseAsync();
        var roomType = await fixture.CreateRoomTypeInDatabaseAsync();
        var startDate = DateTime.Today.AddDays(1);
        var endDate = startDate.AddDays(3);
        var dailyRate = new Money(200.00m, "USD");
        const int roomQuantity = 2;
        const decimal lastMinuteRateIncrease = 1.15m;
        
        var dates = new DateRange(startDate, endDate).GetDates().ToList();
        foreach (var date in dates)
        {
            await fixture.CreateRoomTypeRateInDatabaseAsync(
                ARoomTypeRate()
                    .WithHotelId(hotel.Id)
                    .WithRoomTypeId(roomType.Id)
                    .WithDate(date)
                    .WithRate(dailyRate)
                    .Build());
        }

        var queryString =
            $"?hotelId={hotel.Id}&roomTypeId={roomType.Id}&startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}&roomQuantity={roomQuantity}";
        
        // Act
        var response = await _client.GetAsync($"/v1/rates{queryString}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var output = await response.Content.ReadFromJsonAsync<IEnumerable<GetRateOutput>>(fixture.JsonSettings);
        output.Should().NotBeNullOrEmpty();
        output.Single().Amount.Should().Be(200.00m * dates.Count * roomQuantity * lastMinuteRateIncrease);
        output.Single().Currency.Should().Be("USD");
    }

    public async ValueTask DisposeAsync()
    {
        await fixture.CleanDatabaseAsync();
    }
}