namespace FC4.HotelReservation.Application.Common;

public interface IUnitOfWork
{
    Task CommitAsync(CancellationToken cancellationToken);
}