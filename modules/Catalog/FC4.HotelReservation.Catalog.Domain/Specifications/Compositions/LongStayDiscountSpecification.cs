using FC4.HotelReservation.Catalog.Domain.Specifications.Base;
using FC4.HotelReservation.Catalog.Domain.Specifications.Context;
using FC4.HotelReservation.Catalog.Domain.Specifications.Simple;

namespace FC4.HotelReservation.Catalog.Domain.Specifications.Compositions;

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
