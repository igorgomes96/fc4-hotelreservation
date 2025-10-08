using FC4.HotelReservation.Catalog.Domain.Entities;

namespace FC4.HotelReservation.Catalog.Domain.Repositories;

public interface IRoomTypeRepository
{
    Task<RoomType?> GetByIdAsync(Guid roomTypeId, CancellationToken cancellationToken);
    Task CreateAsync(RoomType roomType, CancellationToken cancellationToken);
}