using System.Text.Json;
using FC4.HotelReservation.Application.UseCases.Rate.GetRate;
using GitHub;
using MediatR;

namespace FC4.HotelReservation.WebApi.Endpoints;

public static class RatesEndpoints
{
    private static bool _isPublisherSet = false;
    public static RouteGroupBuilder MapRatesApi(this RouteGroupBuilder group)
    {
        group.MapGet("/", async (
            Guid hotelId,
            Guid roomTypeId,
            DateTime startDate,
            DateTime endDate,
            int roomQuantity,
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            if (!_isPublisherSet)
            {
                Scientist.ResultPublisher = new ParallelRunPublisher();
                _isPublisherSet = true;
            }

            var input = new GetRateInput(hotelId, roomTypeId, startDate, endDate, roomQuantity);
            var rate = await Scientist.ScienceAsync<IEnumerable<GetRateOutput>>("rate-parallel-run", experiment =>
            {
                experiment.Compare((v1, v2) => v1.First().Amount == v2.First().Amount);
                experiment.Use(async () => await mediator.Send(input, cancellationToken));
                experiment.Try(async () => await GetRateFromMicroservice(input));
            });
            return TypedResults.Ok(rate);
        });

        return group;
    }
    
    private static async Task<IEnumerable<GetRateOutput>> GetRateFromMicroservice(GetRateInput input)
    {
        try
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://catalog-webapi:8080/v1/")
            };

            var response = await httpClient.GetAsync(
                $"rates?hotelId={input.HotelId}&roomTypeId={input.RoomTypeId}&startDate={input.StartDate:yyyy-MM-dd}&endDate={input.EndDate:yyyy-MM-dd}&roomQuantity={input.RoomQuantity}");
            response.EnsureSuccessStatusCode();
            var rates = await response.Content.ReadFromJsonAsync<IEnumerable<GetRateOutput>>();
            return rates ?? [];
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error calling microservice: {ex.Message}");
            throw;
        }
    }
}

public class ParallelRunPublisher : IResultPublisher
{
    public Task Publish<T, TClean>(Result<T, TClean> result)
    {
        Console.WriteLine($"Publishing results for experiment '{result.ExperimentName}'");
        Console.WriteLine($"Result: {(result.Matched ? "MATCH" : "MISMATCH")}");
        Console.WriteLine($"Control value: {JsonSerializer.Serialize(result.Control.Value)}");
        Console.WriteLine($"Control duration: {result.Control.Duration}");
        foreach (var observation in result.Candidates)
        {
            Console.WriteLine($"Candidate name: {observation.Name}");
            Console.WriteLine($"Candidate value: {JsonSerializer.Serialize(observation.Value)}");
            Console.WriteLine($"Candidate duration: {observation.Duration}");
        }

        return Task.FromResult(0);
    }
    
}