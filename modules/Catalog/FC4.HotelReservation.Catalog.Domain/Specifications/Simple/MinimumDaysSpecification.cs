using FC4.HotelReservation.Catalog.Domain.Specifications.Base;

namespace FC4.HotelReservation.Catalog.Domain.Specifications.Simple;

public class MinimumDaysSpecification: Specification<int>
{
    private readonly int _minimumDays;

    public MinimumDaysSpecification(int minimumDays)
    {
        _minimumDays = minimumDays;
    }

    public override bool IsSatisfiedBy(int days)
    {
        return days >= _minimumDays;
    }
}
