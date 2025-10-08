using FC4.HotelReservation.Domain.Services.Interfaces;
using FC4.HotelReservation.Domain.ValueObjects;

namespace FC4.HotelReservation.Application.UseCases.Rate.GetRate;

public class GetRate(IRateService rateService) : IGetRate
{
    public async Task<IEnumerable<GetRateOutput>> Handle(GetRateInput request, CancellationToken cancellationToken)
    {
        var period = new DateRange(request.StartDate, request.EndDate);
        var totalAmount = await rateService.CalculateTotalAmountAsync(
            request.HotelId, request.RoomTypeId, period, request.RoomQuantity, cancellationToken);
        return [new GetRateOutput(totalAmount.Value, totalAmount.Currency)];
    }
}