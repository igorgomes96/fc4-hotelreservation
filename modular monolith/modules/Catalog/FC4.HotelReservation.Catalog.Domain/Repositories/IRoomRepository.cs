using FC4.HotelReservation.Catalog.Domain.Entities;

namespace FC4.HotelReservation.Catalog.Domain.Repositories;

public interface IRoomRepository
{
    Task<Room?> GetByIdAsync(Guid roomId, CancellationToken cancellationToken);
    Task CreateAsync(Room room, CancellationToken cancellationToken);
}