namespace FC4.HotelReservation.Payments.Application;

public interface IUnitOfWork
{
    Task CommitAsync(CancellationToken cancellationToken);
}