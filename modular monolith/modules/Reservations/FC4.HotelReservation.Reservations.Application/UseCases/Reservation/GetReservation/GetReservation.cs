using FC4.HotelReservation.Reservations.Application.UseCases.Reservation.Common;
using FC4.HotelReservation.Reservations.Domain.Repositories;
using FC4.HotelReservation.Shared.Application.Exceptions;

namespace FC4.HotelReservation.Reservations.Application.UseCases.Reservation.GetReservation;

public class GetReservation(IReservationRepository reservationRepository) : IGetReservation
{
    public async Task<ReservationOutput> Handle(GetReservationInput request, CancellationToken cancellationToken)
    {
        var reservation = await reservationRepository.GetByIdAsync(request.ReservationId, cancellationToken)
                          ?? throw new NotFoundException("Reservation not found");

        return ReservationOutput.FromReservation(reservation);
    }
}