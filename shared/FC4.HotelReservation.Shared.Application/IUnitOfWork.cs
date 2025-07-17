namespace FC4.HotelReservation.Shared.Application;

public interface IUnitOfWork
{
    Task CommitAsync(CancellationToken cancellationToken);
}