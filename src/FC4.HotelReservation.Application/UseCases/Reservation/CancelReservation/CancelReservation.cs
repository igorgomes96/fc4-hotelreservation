using FC4.HotelReservation.Domain.Repositories;

namespace FC4.HotelReservation.Application.UseCases.Reservation.CancelReservation;

public class CancelReservation(IReservationRepository reservationRepository) : ICancelReservation
{
    public async Task Handle(CancelReservationInput request, CancellationToken cancellationToken)
    {
        var reservation = await reservationRepository.GetByIdAsync(request.ReservationId, cancellationToken)
                          ?? throw new InvalidOperationException("Reservation not found");
        reservation.Cancel();
        await reservationRepository.UpdateAsync(reservation, cancellationToken);
    }
}