using FC4.HotelReservation.Application.Common;
using FC4.HotelReservation.Catalog.Domain.Repositories;

namespace FC4.HotelReservation.Application.UseCases.Hotel.CreateHotel;

public class CreateHotel(IHotelRepository repository, IUnitOfWork unitOfWork) : ICreateHotel
{
    public async Task<CreateHotelOutput> Handle(CreateHotelInput request, CancellationToken cancellationToken)
    {
        var hotel = request.ToHotel();
        await repository.CreateHotelAsync(hotel, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
        return new CreateHotelOutput(hotel.Id);
    }
}