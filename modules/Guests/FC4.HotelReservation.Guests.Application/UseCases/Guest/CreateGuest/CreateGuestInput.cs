using MediatR;

namespace FC4.HotelReservation.Guests.Application.UseCases.Guest.CreateGuest;

public record CreateGuestInput(
    string FirstName,
    string LastName,
    string Email
) : IRequest<CreateGuestOutput>
{
    public Guests.Domain.Entities.Guest ToGuest()
    {
        return Guests.Domain.Entities.Guest.Create(
            FirstName,
            LastName,
            Email);
    }
}