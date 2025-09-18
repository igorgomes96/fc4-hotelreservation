using FC4.HotelReservation.Catalog.Application.UseCases.Rate.GetRate;
using FC4.HotelReservation.Reservations.Application.Gateways;
using FC4.HotelReservation.Reservations.Domain.ValueObjects;
using MediatR;

namespace FC4.HotelReservation.Reservations.Adapters.Adapters;

public class CatalogRateService(IMediator mediator) : ICatalogRateService
{
    public async Task<Money> CalculateTotalAmountAsync(
        Guid hotelId,
        Guid roomTypeId,
        DateRange stayPeriod,
        int roomQuantity,
        CancellationToken cancellationToken)
    {
        var rates = await mediator.Send(new GetRateInput(
                hotelId,
                roomTypeId,
                stayPeriod.StartDate,
                stayPeriod.EndDate,
                roomQuantity),
            cancellationToken);
        var rate = rates.First();
        return new Money(rate.Amount, rate.Currency);
    }
}