using FC4.HotelReservation.Catalog.Domain.Specifications.Base;
using FC4.HotelReservation.Catalog.Domain.Specifications.Context;

namespace FC4.HotelReservation.Catalog.Domain.Specifications.Compositions;

public class StandardDiscountSpecification: Specification<ReservationContext>
{
    private readonly Specification<ReservationContext> _composedSpec;

    public StandardDiscountSpecification()
    {
        var longStaySpec = new LongStayDiscountSpecification();
        var volumeDiscountSpec = new VolumeDiscountSpecification();
        
        _composedSpec = longStaySpec.Or(volumeDiscountSpec);
    }

    public override bool IsSatisfiedBy(ReservationContext reservation)
    {
        return _composedSpec.IsSatisfiedBy(reservation);
    }
}