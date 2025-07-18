namespace FC4.HotelReservation.Reservations.Application;

public interface IUnitOfWork
{
    Task CommitAsync(CancellationToken cancellationToken);
}