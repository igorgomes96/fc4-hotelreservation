using FC4.HotelReservation.Domain.Entities;

namespace FC4.HotelReservation.Domain.Repositories;

public interface IHotelRepository
{
    Task<Hotel?> GetHotelByIdAsync(Guid hotelId, CancellationToken cancellationToken); 
    Task CreateHotelAsync(Hotel hotel, CancellationToken cancellationToken);
}