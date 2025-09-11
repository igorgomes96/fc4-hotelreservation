using Ardalis.GuardClauses;

namespace FC4.HotelReservation.Catalog.Domain.ValueObjects;

public record DateRange
{
    public DateRange(DateTime startDate, DateTime endDate)
    {
        StartDate = Guard.Against.Default(startDate, nameof(startDate));
        EndDate = Guard.Against.Default(endDate, nameof(endDate));
        
        if (startDate >= endDate)
            throw new ArgumentException("Start date must be before end date");
    }
    
    public DateTime StartDate { get; }
    public DateTime EndDate { get; }

    public int NightCount => (EndDate - StartDate).Days;

    public IEnumerable<DateTime> GetDates()
    {
        var currentDate = StartDate.Date;
        var endDate = EndDate.Date;
    
        while (currentDate < endDate)
        {
            yield return currentDate;
            currentDate = currentDate.AddDays(1);
        }
    }
}