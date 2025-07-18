using FC4.HotelReservation.Payments.Application;
using FC4.HotelReservation.Shared.Domain;
using MediatR;

namespace FC4.HotelReservation.Payments.Infra.Data;

public class UnitOfWork(
    PaymentsDbContext dbContext,
    IPublisher publisher) : IUnitOfWork
{
    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        // Esse loop é ncessário pois a manipulação de eventos pode gerar novos eventos
        while (true)
        {
            var aggregateRoots = dbContext.ChangeTracker
                .Entries<AggregateRoot>()
                .Where(entry => entry.Entity.Events.Count > 0)
                .Select(entry => entry.Entity)
                .ToList();

            if (aggregateRoots.Count == 0)
            {
                break;
            }

            foreach (var aggregate in aggregateRoots)
            {
                foreach (var @event in aggregate.Events.ToList())
                {
                    await publisher.Publish((dynamic)@event, cancellationToken);
                    aggregate.RemoveEvent(@event);
                }
            }
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}