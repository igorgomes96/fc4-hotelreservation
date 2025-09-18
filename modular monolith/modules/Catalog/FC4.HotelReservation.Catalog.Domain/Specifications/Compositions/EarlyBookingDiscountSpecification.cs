using FC4.HotelReservation.Catalog.Domain.Specifications.Base;
using FC4.HotelReservation.Catalog.Domain.Specifications.Context;
using FC4.HotelReservation.Catalog.Domain.Specifications.Simple;

namespace FC4.HotelReservation.Catalog.Domain.Specifications.Compositions;

public class EarlyBookingDiscountSpecification : Specification<ReservationContext>
{
    private readonly Specification<int> _minimumAdvanceSpec;

    public EarlyBookingDiscountSpecification()
    {
        _minimumAdvanceSpec = new MinimumDaysSpecification(30);
    }

    public override bool IsSatisfiedBy(ReservationContext reservation)
    {
        var daysInAdvance = (reservation.StayPeriod.StartDate - reservation.CreatedAt).Days;
        return _minimumAdvanceSpec.IsSatisfiedBy(daysInAdvance);
    }
}
