using FC4.HotelReservation.Domain.Entities;

namespace FC4.HotelReservation.Domain.Repositories;

public interface IRoomRepository
{
    Task<Room?> GetByIdAsync(Guid roomId, CancellationToken cancellationToken);
    Task CreateAsync(Room room, CancellationToken cancellationToken);
}