using FC4.HotelReservation.Reservations.Application.Gateways;
using FC4.HotelReservation.Reservations.Domain.Entities;
using FC4.HotelReservation.Reservations.Domain.Repositories;
using FC4.HotelReservation.Reservations.Domain.ValueObjects;
using FC4.HotelReservation.Shared.Application;

namespace FC4.HotelReservation.Reservations.Application.UseCases.Reservation.CreateReservation;

public class CreateReservation(
    IReservationRepository reservationRepository,
    IRoomTypeInventoryRepository roomTypeInventoryRepository,
    ICatalogRateService rateService,
    IReservationGuestRepository guestRepository,
    IUnitOfWork unitOfWork) : ICreateReservation
{
    public async Task<CreateReservationOutput> Handle(
        CreateReservationInput request,
        CancellationToken cancellationToken)
    {
        _ = await guestRepository.GetByIdAsync(request.GuestId, cancellationToken)
            ?? throw new InvalidOperationException("Guest not found");
        
        var period = new DateRange(request.StartDate, request.EndDate);
        var inventories = await roomTypeInventoryRepository.GetInventoryForPeriodAsync(
            request.HotelId, request.RoomTypeId, period, cancellationToken);

        if (!HasSufficientInventory(inventories, period, request.RoomQuantity))
        {
            throw new InvalidOperationException("Not enough rooms available for the requested period");
        }

        var totalAmount = await rateService.CalculateTotalAmountAsync(
            request.HotelId, request.RoomTypeId, period, request.RoomQuantity, cancellationToken);
        var reservation = request.ToReservation(totalAmount);
        await reservationRepository.CreateAsync(reservation, cancellationToken);

        foreach (var inventory in inventories)
        {
            inventory.ReserveRooms(request.RoomQuantity);
            await roomTypeInventoryRepository.UpdateAsync(inventory, cancellationToken);
        }

        await unitOfWork.CommitAsync(cancellationToken);
        return new CreateReservationOutput(reservation.Id);
    }

    private static bool HasSufficientInventory(
        List<RoomTypeInventory> inventories,
        DateRange period,
        int roomQuantity)
    {
        var daysInPeriod = period.GetDates().ToList();
        var inventoryDict = inventories.ToDictionary(i => i.Date.Date);
        return daysInPeriod.All(date =>
            inventoryDict.TryGetValue(date, out var inventory) &&
            inventory.CanReserve(roomQuantity));
    }
}