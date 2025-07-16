using FC4.HotelReservation.Catalog.Application.UseCases.Room.CreateRoom;
using FC4.HotelReservation.Catalog.Application.UseCases.Room.GetRoom;
using MediatR;

namespace FC4.HotelReservation.WebApi.Endpoints;

public static class RoomsEndpoints
{
    public static RouteGroupBuilder MapRoomsApi(this RouteGroupBuilder group)
    {
        group.MapPost("/", async (CreateRoomInput input, IMediator mediator) =>
        {
            var output = await mediator.Send(input);
            return TypedResults.Created($"/rooms/{output.Id}", output);
        });

        group.MapGet("/{id:guid}", async (Guid id, IMediator mediator) =>
            TypedResults.Ok(await mediator.Send(new GetRoomInput(id))));

        return group;
    }
}