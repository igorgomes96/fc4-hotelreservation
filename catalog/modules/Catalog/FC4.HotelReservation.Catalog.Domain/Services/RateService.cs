using FC4.HotelReservation.Catalog.Domain.Entities;
using FC4.HotelReservation.Catalog.Domain.Repositories;
using FC4.HotelReservation.Catalog.Domain.Services.Interfaces;
using FC4.HotelReservation.Catalog.Domain.Specifications.Compositions;
using FC4.HotelReservation.Catalog.Domain.Specifications.Context;
using FC4.HotelReservation.Catalog.Domain.ValueObjects;

namespace FC4.HotelReservation.Catalog.Domain.Services;

public class RateService(IRoomTypeRateRepository roomTypeRateRepository) : IRateService
{
    private readonly PremiumDiscountSpecification _premiumDiscountSpec = new();
    private readonly StandardDiscountSpecification _standardDiscountSpec = new();
    private readonly LastMinuteBookingSpecification _lastMinuteSpec = new();

    public async Task<Money> CalculateTotalAmountAsync(
        Guid hotelId,
        Guid roomTypeId,
        DateRange stayPeriod,
        int roomQuantity,
        CancellationToken cancellationToken)
    {
        var rates = await roomTypeRateRepository.GetRateForPeriodAsync(
            hotelId, roomTypeId, stayPeriod, cancellationToken);
        var baseAmount = CalculateBaseAmount(rates, roomQuantity);
        var reservationContext = new ReservationContext(stayPeriod, roomQuantity, DateTime.UtcNow);
        var adjustedAmount = ApplyPriceAdjustments(baseAmount, reservationContext);
        return adjustedAmount;
    }

    private static Money CalculateBaseAmount(IEnumerable<RoomTypeRate> rates, int roomQuantity)
    {
        var totalBaseRate = rates.Sum(rate => rate.Rate.Value);
        var currency = rates.First().Rate.Currency;
        return new Money(totalBaseRate * roomQuantity, currency);
    }

    private Money ApplyPriceAdjustments(Money baseAmount, ReservationContext reservation)
    {
        var adjustedAmount = baseAmount.Value;

        if (_premiumDiscountSpec.IsSatisfiedBy(reservation))
        {
            adjustedAmount *= 0.80m; // 20% desconto
        }
        else if (_standardDiscountSpec.IsSatisfiedBy(reservation))
        {
            adjustedAmount *= 0.90m; // 10% desconto
        }

        if (_lastMinuteSpec.IsSatisfiedBy(reservation))
        {
            adjustedAmount *= 1.15m; // 15% aumento
        }

        return new Money(adjustedAmount, baseAmount.Currency);
    }
}