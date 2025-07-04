using FC4.HotelReservation.Domain.Entities;
using FC4.HotelReservation.Domain.Specifications.Base;
using FC4.HotelReservation.Domain.Specifications.Context;
using FC4.HotelReservation.Domain.Specifications.Simple;

namespace FC4.HotelReservation.Domain.Specifications.Compositions;

public class LongStayDiscountSpecification: Specification<ReservationContext>
{
    private readonly Specification<int> _minimumDaysSpec;

    public LongStayDiscountSpecification()
    {
        _minimumDaysSpec = new MinimumDaysSpecification(7);
    }

    public override bool IsSatisfiedBy(ReservationContext reservation)
    {
        var days = reservation.StayPeriod.NightCount;
        return _minimumDaysSpec.IsSatisfiedBy(days);
    }
}
