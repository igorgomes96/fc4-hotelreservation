namespace FC4.HotelReservation.Catalog.Domain.Specifications.Base;

public interface ISpecification<in T>
{
    bool IsSatisfiedBy(T entity);
}