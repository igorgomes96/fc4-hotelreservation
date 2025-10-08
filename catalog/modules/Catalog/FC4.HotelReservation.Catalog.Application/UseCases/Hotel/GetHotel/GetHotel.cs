using FC4.HotelReservation.Catalog.Domain.Repositories;
using FC4.HotelReservation.Shared.Application.Exceptions;

namespace FC4.HotelReservation.Catalog.Application.UseCases.Hotel.GetHotel;

public class GetHotel(IHotelRepository repository) : IGetHotel
{
    public async Task<GetHotelOutput> Handle(GetHotelInput request, CancellationToken cancellationToken)
    {
        var hotel = await repository.GetByIdAsync(request.HotelId, cancellationToken)
                    ?? throw new NotFoundException("Hotel not found");
        return GetHotelOutput.FromHotel(hotel);
    }
}