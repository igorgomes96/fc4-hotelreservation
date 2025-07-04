using FC4.HotelReservation.Domain.Entities;
using FC4.HotelReservation.Domain.Specifications.Compositions;
using FC4.HotelReservation.Domain.Specifications.Context;
using FC4.HotelReservation.Domain.ValueObjects;

namespace FC4.HotelReservation.Domain.Services;

public class RateService
{
    private readonly PremiumDiscountSpecification _premiumDiscountSpec;
    private readonly StandardDiscountSpecification _standardDiscountSpec;
    private readonly LastMinuteBookingSpecification _lastMinuteSpec;

    public RateService()
    {
        _premiumDiscountSpec = new PremiumDiscountSpecification();
        _standardDiscountSpec = new StandardDiscountSpecification();
        _lastMinuteSpec = new LastMinuteBookingSpecification();
    }

    public Money CalculateTotalAmountAsync(
        DateRange stayPeriod,
        int roomQuantity, 
        IEnumerable<RoomTypeRate> rates)
    {
        var baseAmount = CalculateBaseAmount(rates, roomQuantity);
        var reservationContext = new ReservationContext(stayPeriod, roomQuantity, DateTime.UtcNow);
        var adjustedAmount = ApplyPriceAdjustments(baseAmount, reservationContext);
        return adjustedAmount;
    }

    private static Money CalculateBaseAmount(IEnumerable<RoomTypeRate> rates, int roomQuantity)
    {
        var totalBaseRate = rates.Sum(rate => rate.Rate.Amount);
        var currency = rates.First().Rate.Currency;
        return new Money(totalBaseRate * roomQuantity, currency);
    }

    private Money ApplyPriceAdjustments(Money baseAmount, ReservationContext reservation)
    {
        var adjustedAmount = baseAmount.Amount;

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