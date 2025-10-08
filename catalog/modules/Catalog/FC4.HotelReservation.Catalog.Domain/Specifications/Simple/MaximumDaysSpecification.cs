using FC4.HotelReservation.Catalog.Domain.Specifications.Base;

namespace FC4.HotelReservation.Catalog.Domain.Specifications.Simple;

public class MaximumDaysSpecification : Specification<int>
{
    private readonly int _maximumDays;

    public MaximumDaysSpecification(int maximumDays)
    {
        _maximumDays = maximumDays;
    }

    public override bool IsSatisfiedBy(int days)
    {
        return days <= _maximumDays;
    }
}