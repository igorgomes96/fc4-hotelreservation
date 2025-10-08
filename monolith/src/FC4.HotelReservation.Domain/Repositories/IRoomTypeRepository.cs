using FC4.HotelReservation.Domain.Entities;

namespace FC4.HotelReservation.Domain.Repositories;

public interface IRoomTypeRepository
{
    Task<RoomType?> GetByIdAsync(Guid roomTypeId, CancellationToken cancellationToken);
    Task CreateAsync(RoomType roomType, CancellationToken cancellationToken);
}