using FC4.HotelReservation.Catalog.Domain.Specifications.Base;
using FC4.HotelReservation.Catalog.Domain.Specifications.Context;
using FC4.HotelReservation.Catalog.Domain.Specifications.Simple;

namespace FC4.HotelReservation.Catalog.Domain.Specifications.Compositions;

public class VolumeDiscountSpecification: Specification<ReservationContext>
{
    private readonly Specification<int> _minimumRoomsSpec;

    public VolumeDiscountSpecification()
    {
        _minimumRoomsSpec = new MinimumRoomsSpecification(3);
    }

    public override bool IsSatisfiedBy(ReservationContext reservation)
    {
        return _minimumRoomsSpec.IsSatisfiedBy(reservation.RoomQuantity);
    }
}