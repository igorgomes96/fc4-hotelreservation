using FC4.HotelReservation.Domain.Entities;

namespace FC4.HotelReservation.Domain.Repositories;

public interface IReservationRepository
{
    Task<Reservation?> GetByIdAsync(Guid reservationId, CancellationToken cancellationToken);
    Task<IEnumerable<Reservation>> GetByGuestIdAsync(Guid guestId, CancellationToken cancellationToken);
    Task CreateAsync(Reservation reservation, CancellationToken cancellationToken);
    Task UpdateAsync(Reservation reservation, CancellationToken cancellationToken);
}