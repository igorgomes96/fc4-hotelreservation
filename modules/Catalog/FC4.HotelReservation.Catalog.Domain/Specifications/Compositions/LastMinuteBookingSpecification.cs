using FC4.HotelReservation.Catalog.Domain.Specifications.Base;
using FC4.HotelReservation.Catalog.Domain.Specifications.Context;
using FC4.HotelReservation.Catalog.Domain.Specifications.Simple;

namespace FC4.HotelReservation.Catalog.Domain.Specifications.Compositions;

public class LastMinuteBookingSpecification : Specification<ReservationContext>
{
    private readonly Specification<int> _maximumAdvanceSpec;

    public LastMinuteBookingSpecification()
    {
        _maximumAdvanceSpec = new MaximumDaysSpecification(3);
    }

    public override bool IsSatisfiedBy(ReservationContext reservation)
    {
        var daysInAdvance = (reservation.StayPeriod.StartDate - reservation.CreatedAt).Days;
        return _maximumAdvanceSpec.IsSatisfiedBy(daysInAdvance);
    }
}