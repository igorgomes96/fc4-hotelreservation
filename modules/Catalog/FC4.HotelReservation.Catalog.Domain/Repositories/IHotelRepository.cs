using FC4.HotelReservation.Catalog.Domain.Entities;

namespace FC4.HotelReservation.Catalog.Domain.Repositories;

public interface IHotelRepository
{
    Task<Hotel?> GetByIdAsync(Guid hotelId, CancellationToken cancellationToken); 
    Task CreateHotelAsync(Hotel hotel, CancellationToken cancellationToken);
}