using FC4.HotelReservation.Domain.Entities;
using FC4.HotelReservation.Domain.Specifications.Base;
using FC4.HotelReservation.Domain.Specifications.Context;
using FC4.HotelReservation.Domain.Specifications.Simple;

namespace FC4.HotelReservation.Domain.Specifications.Compositions;

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