using MediatR;

namespace FC4.HotelReservation.Application.UseCases.Guest.CreateGuest;

public record CreateGuestInput(
    string FirstName,
    string LastName,
    string Email
) : IRequest<CreateGuestOutput>
{
    public Domain.Entities.Guest ToGuest()
    {
        return new Domain.Entities.Guest(
            FirstName,
            LastName,
            Email);
    }
}