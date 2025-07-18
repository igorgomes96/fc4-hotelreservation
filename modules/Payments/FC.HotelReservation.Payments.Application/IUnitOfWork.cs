namespace FC.HotelReservation.Payments.Application;

public interface IUnitOfWork
{
    Task CommitAsync(CancellationToken cancellationToken);
}