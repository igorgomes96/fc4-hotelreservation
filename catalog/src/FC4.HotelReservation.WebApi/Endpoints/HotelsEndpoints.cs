using FC4.HotelReservation.Catalog.Application.UseCases.Hotel.CreateHotel;
using FC4.HotelReservation.Catalog.Application.UseCases.Hotel.GetHotel;
using MediatR;

namespace FC4.HotelReservation.WebApi.Endpoints;

public static class HotelsEndpoints
{
    public static RouteGroupBuilder MapHotelsApi(this RouteGroupBuilder group)
    {
        group.MapPost("/", async (CreateHotelInput input, IMediator mediator) =>
        {
            var output = await mediator.Send(input);
            return TypedResults.Created($"/hotels/{output.Id}", output);
        });

        group.MapGet("/{id:guid}", async (Guid id, IMediator mediator) => 
            TypedResults.Ok(await mediator.Send(new GetHotelInput(id))));

        return group;
    }
}