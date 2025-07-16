using FC4.HotelReservation.Catalog.Application.UseCases.Rate.GetRate;
using MediatR;

namespace FC4.HotelReservation.WebApi.Endpoints;

public static class RatesEndpoints
{
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
            var rate = await mediator.Send(new GetRateInput(hotelId, roomTypeId, startDate, endDate, roomQuantity),
                cancellationToken);
            return TypedResults.Ok(rate);
        });

        return group;
    }
}