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
        return Domain.Entities.Guest.Create(
            FirstName,
            LastName,
            Email);
    }
}