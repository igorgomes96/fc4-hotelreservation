using FC4.HotelReservation.Catalog.Domain.ValueObjects;
using MediatR;

namespace FC4.HotelReservation.Catalog.Application.UseCases.Hotel.CreateHotel;

public record CreateHotelInput(
    string Name,
    string Street,
    string City,
    string State,
    string Country,
    string ZipCode
) : IRequest<CreateHotelOutput>
{
    public Catalog.Domain.Entities.Hotel ToHotel()
    {
        return Catalog.Domain.Entities.Hotel.Create(
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