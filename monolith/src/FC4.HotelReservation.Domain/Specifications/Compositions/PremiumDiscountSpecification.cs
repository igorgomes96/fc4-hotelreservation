using FC4.HotelReservation.Domain.Entities;
using FC4.HotelReservation.Domain.Specifications.Base;
using FC4.HotelReservation.Domain.Specifications.Context;

namespace FC4.HotelReservation.Domain.Specifications.Compositions;

public class PremiumDiscountSpecification : Specification<ReservationContext>
{
    private readonly Specification<ReservationContext> _composedSpec;

    public PremiumDiscountSpecification()
    {
        var longStaySpec = new LongStayDiscountSpecification();
        var volumeDiscountSpec = new VolumeDiscountSpecification();
        var earlyBookingSpec = new EarlyBookingDiscountSpecification();
        
        _composedSpec = longStaySpec.And(volumeDiscountSpec).And(earlyBookingSpec);
    }

    public override bool IsSatisfiedBy(ReservationContext reservation)
    {
        return _composedSpec.IsSatisfiedBy(reservation);
    }
}
