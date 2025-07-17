using FC4.HotelReservation.Reservations.Domain.Events;
using FC4.HotelReservation.Reservations.Domain.Repositories;
using MediatR;

namespace FC4.HotelReservation.Reservations.Application.EventHandlers;

public class ReservationCanceledEventHandler(IRoomTypeInventoryRepository roomTypeInventoryRepository)
    : INotificationHandler<ReservationCanceledEvent>
{
    public async Task Handle(ReservationCanceledEvent notification, CancellationToken cancellationToken)
    {
        var inventories = await roomTypeInventoryRepository.GetInventoryForPeriodAsync(
            notification.HotelId, notification.RoomTypeId, notification.StayPeriod, cancellationToken);
        
        foreach (var inventory in inventories)
        {
            inventory.ReleaseRooms(notification.RoomQuantity);
            await roomTypeInventoryRepository.UpdateAsync(inventory, cancellationToken);
        }
    }
}