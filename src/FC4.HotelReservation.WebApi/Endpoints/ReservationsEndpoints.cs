using FC4.HotelReservation.Reservations.Application.UseCases.Reservation.CancelReservation;
using FC4.HotelReservation.Reservations.Application.UseCases.Reservation.CreateReservation;
using FC4.HotelReservation.Reservations.Application.UseCases.Reservation.GetReservation;
using FC4.HotelReservation.Reservations.Application.UseCases.Reservation.ListReservations;
using MediatR;

namespace FC4.HotelReservation.WebApi.Endpoints;

public static class ReservationsEndpoints
{
    public static RouteGroupBuilder MapReservationsApi(this RouteGroupBuilder group)
    {
        group.MapGet("/", async (Guid guestId, IMediator mediator) => 
            TypedResults.Ok(await mediator.Send(new ListReservationsInput(guestId))));

        group.MapGet("/{id:guid}", async (Guid id, IMediator mediator) =>
            TypedResults.Ok(await mediator.Send(new GetReservationInput(id))));

        group.MapPost("/", async (CreateReservationInput input, IMediator mediator) =>
        {
            var output = await mediator.Send(input);
            return TypedResults.Created($"/reservations/{output.Id}", output);
        });

        group.MapDelete("/{id:guid}", async (Guid id, IMediator mediator) =>
        {
            await mediator.Send(new CancelReservationInput(id));
            return TypedResults.NoContent();
        });

        return group;
    }
}