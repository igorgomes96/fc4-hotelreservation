using FC4.HotelReservation.Reservations.Domain.Entities;

namespace FC4.HotelReservation.Reservations.Domain.Repositories;

public interface IReservationRepository
{
    Task<Reservation?> GetByIdAsync(Guid reservationId, CancellationToken cancellationToken);
    Task<IEnumerable<Reservation>> GetByGuestIdAsync(Guid guestId, CancellationToken cancellationToken);
    Task CreateAsync(Reservation reservation, CancellationToken cancellationToken);
    Task UpdateAsync(Reservation reservation, CancellationToken cancellationToken);
}