namespace FC4.HotelReservation.Catalog.Application.UseCases.Hotel.GetHotel;

public record GetHotelOutput(
    Guid Id,
    string Name,
    string Street,
    string City,
    string State,
    string Country,
    string ZipCode
)
{
    public static GetHotelOutput FromHotel(Catalog.Domain.Entities.Hotel hotel)
    {
        return new GetHotelOutput(
            hotel.Id,
            hotel.Name,
            hotel.Address.Street,
            hotel.Address.City,
            hotel.Address.State,
            hotel.Address.Country,
            hotel.Address.ZipCode
        );
    }
}
