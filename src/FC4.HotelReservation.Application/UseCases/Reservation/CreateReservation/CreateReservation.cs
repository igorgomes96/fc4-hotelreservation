using FC4.HotelReservation.Application.Common;
using FC4.HotelReservation.Domain.Repositories;
using FC4.HotelReservation.Domain.Services.Interfaces;
using FC4.HotelReservation.Domain.ValueObjects;

namespace FC4.HotelReservation.Application.UseCases.Reservation.CreateReservation;

public class CreateReservation(
    IReservationRepository reservationRepository,
    IRoomTypeRateRepository roomTypeRateRepository,
    IRateService rateService,
    IUnitOfWork unitOfWork) : ICreateReservation
{
    public async Task<CreateReservationOutput> Handle(
        CreateReservationInput request,
        CancellationToken cancellationToken)
    {
        var period = new DateRange(request.StartDate, request.EndDate);
        var rates = await roomTypeRateRepository.GetRateForPeriodAsync(
            request.HotelId, request.RoomTypeId, period, cancellationToken);
        var totalAmount = rateService.CalculateTotalAmountAsync(period, request.RoomQuantity, rates);
        var reservation = request.ToReservation(totalAmount);
        await reservationRepository.CreateAsync(reservation, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
        return new CreateReservationOutput(reservation.Id);
    }
}