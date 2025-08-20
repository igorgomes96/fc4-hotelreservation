using FC4.HotelReservation.Catalog.Domain.Specifications.Base;

namespace FC4.HotelReservation.Catalog.Domain.Specifications.Simple;

public class MinimumRoomsSpecification: Specification<int>
{
    private readonly int _minimumRooms;

    public MinimumRoomsSpecification(int minimumRooms)
    {
        _minimumRooms = minimumRooms;
    }

    public override bool IsSatisfiedBy(int roomQuantity)
    {
        return roomQuantity >= _minimumRooms;
    }
}