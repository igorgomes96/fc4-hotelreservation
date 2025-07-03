using FC4.HotelReservation.Domain.Repositories;

namespace FC4.HotelReservation.Application.UseCases.Hotel.CreateHotel;

public class CreateHotel(IHotelRepository repository) : ICreateHotel
{
    public async Task<CreateHotelOutput> Handle(CreateHotelInput request, CancellationToken cancellationToken)
    {
        var hotel = request.ToHotel();
        await repository.CreateHotelAsync(hotel, cancellationToken);
        return new CreateHotelOutput(hotel.Id);
    }
}