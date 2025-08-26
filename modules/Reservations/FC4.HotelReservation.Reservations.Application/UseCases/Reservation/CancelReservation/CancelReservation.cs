using FC4.HotelReservation.Application.Common;
using FC4.HotelReservation.Application.Exceptions;
using FC4.HotelReservation.Domain.Repositories;

namespace FC4.HotelReservation.Reservations.Application.UseCases.Reservation.CancelReservation;

public class CancelReservation(IReservationRepository reservationRepository, IUnitOfWork unitOfWork) : ICancelReservation
{
    public async Task Handle(CancelReservationInput request, CancellationToken cancellationToken)
    {
        var reservation = await reservationRepository.GetByIdAsync(request.ReservationId, cancellationToken)
                          ?? throw new NotFoundException("Reservation not found");
        reservation.Cancel();
        await reservationRepository.UpdateAsync(reservation, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
    }
}