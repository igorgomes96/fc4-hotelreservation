namespace FC4.HotelReservation.Catalog.Application;

public interface IUnitOfWork
{
    Task CommitAsync(CancellationToken cancellationToken);
}