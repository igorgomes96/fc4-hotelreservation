using FC4.HotelReservation.Domain.Entities;

namespace FC4.HotelReservation.Domain.Repositories;

public interface IGuestRepository
{
    Task<Guest?> GetByIdAsync(Guid hotelId, CancellationToken cancellationToken); 
    Task CreateGuestAsync(Guest guest, CancellationToken cancellationToken);
}