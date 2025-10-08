namespace FC4.HotelReservation.Domain.Specifications.Base;

public interface ISpecification<in T>
{
    bool IsSatisfiedBy(T entity);
}