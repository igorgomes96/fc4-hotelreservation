using FC4.HotelReservation.Reservations.Domain.Entities;

namespace FC4.HotelReservation.Reservations.Domain.Repositories;

public interface IReservationGuestRepository
{
    Task<GuestInfo?> GetByIdAsync(Guid hotelId, CancellationToken cancellationToken); 
}