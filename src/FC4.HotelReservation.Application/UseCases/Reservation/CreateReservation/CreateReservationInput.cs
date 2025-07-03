using FC4.HotelReservation.Domain.ValueObjects;
using MediatR;

namespace FC4.HotelReservation.Application.UseCases.Reservation.CreateReservation;

public record CreateReservationInput(
    Guid HotelId,
    Guid RoomTypeId,
    DateTime StartDate,
    DateTime EndDate,
    Guid GuestId,
    int RoomQuantity,
    decimal Amount,
    string Currency
) : IRequest<CreateReservationOutput>
{
    public Domain.Entities.Reservation ToReservation()
    {
        var stayPeriod = new DateRange(StartDate, EndDate);
        var totalAmount = new Money(Amount, Currency);
        
        return new Domain.Entities.Reservation(HotelId, RoomTypeId, stayPeriod, GuestId, RoomQuantity, totalAmount);
    }
}
