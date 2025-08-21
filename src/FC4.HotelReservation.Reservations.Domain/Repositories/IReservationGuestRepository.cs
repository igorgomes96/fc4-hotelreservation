using FC4.HotelReservation.Domain.Entities;

namespace FC4.HotelReservation.Domain.Repositories;

public interface IReservationGuestRepository
{
    Task<GuestInfo?> GetByIdAsync(Guid hotelId, CancellationToken cancellationToken); 
    Task CreateGuestAsync(GuestInfo guest, CancellationToken cancellationToken);
}