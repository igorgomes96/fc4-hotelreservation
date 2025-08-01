using FC4.HotelReservation.Domain.ValueObjects;
using MediatR;

namespace FC4.HotelReservation.Application.UseCases.Hotel.CreateHotel;

public record CreateHotelInput(
    string Name,
    string Street,
    string City,
    string State,
    string Country,
    string ZipCode
) : IRequest<CreateHotelOutput>
{
    public Domain.Entities.Hotel ToHotel()
    {
        return Domain.Entities.Hotel.Create(
            Name,
            new Address(
                Street,
                City,
                State,
                Country,
                ZipCode)
        );
    }
}