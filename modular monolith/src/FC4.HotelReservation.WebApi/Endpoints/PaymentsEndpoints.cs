using FC4.HotelReservation.WebApi.Models;
using MediatR;

namespace FC4.HotelReservation.WebApi.Endpoints;

public static class PaymentsEndpoints
{
    public static RouteGroupBuilder MapPaymentsApi(this RouteGroupBuilder group)
    {
        group.MapPatch("/{id:guid}", async (Guid id, UpdatePaymentStatusRequest request, IMediator mediator) =>
        {
            await mediator.Send(request.ToInput(id));
            return TypedResults.NoContent();
        });

        return group;
    }
}