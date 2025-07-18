using FC4.HotelReservation.Catalog.Domain.Services;
using FC4.HotelReservation.Catalog.Domain.ValueObjects;

namespace FC4.HotelReservation.Catalog.Application.UseCases.Rate.GetRate;

public class GetRate(RateService rateService) : IGetRate
{
    public async Task<IEnumerable<GetRateOutput>> Handle(GetRateInput request, CancellationToken cancellationToken)
    {
        var period = new DateRange(request.StartDate, request.EndDate);
        var totalAmount = await rateService.CalculateTotalAmountAsync(
            request.HotelId, request.RoomTypeId, period, request.RoomQuantity, cancellationToken);
        return [new GetRateOutput(totalAmount.Value, totalAmount.Currency)];
    }
}