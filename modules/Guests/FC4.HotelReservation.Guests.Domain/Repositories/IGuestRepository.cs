using FC4.HotelReservation.Guests.Domain.Entities;

namespace FC4.HotelReservation.Guests.Domain.Repositories;

public interface IGuestRepository
{
    Task CreateGuestAsync(Guest guest, CancellationToken cancellationToken);
}