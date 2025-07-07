using FC4.HotelReservation.Application.Common;
using FC4.HotelReservation.Domain.Common;
using MediatR;

namespace FC4.HotelReservation.Infrastructure;

public class UnitOfWork(
    HotelDbContext dbContext,
    IPublisher publisher) : IUnitOfWork
{
    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        var aggregateRoots = dbContext.ChangeTracker
            .Entries<AggregateRoot>()
            .Where(entry => entry.Entity.Events.Any())
            .Select(entry => entry.Entity)
            .ToList();

        var events = aggregateRoots
            .SelectMany(aggregate => aggregate.Events)
            .ToList();

        foreach (var @event in events)
        {
            await publisher.Publish((dynamic)@event, cancellationToken);
        }

        foreach (var aggregate in aggregateRoots)
        {
            aggregate.ClearEvents();
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }
    
}