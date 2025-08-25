using FC4.HotelReservation.Guests.Application.UseCases.Guest.CreateGuest;
using MediatR;

namespace FC4.HotelReservation.WebApi.Endpoints;

public static class GuestsEndpoints
{
    public static RouteGroupBuilder MapGuestsApi(this RouteGroupBuilder group)
    {
        group.MapPost("/", async (CreateGuestInput input, IMediator mediator) =>
        {
            var output = await mediator.Send(input);
            return TypedResults.Created($"/guests/{output.Id}", output);
        });

        return group;
    }
}