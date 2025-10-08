using FC4.HotelReservation.Application.Exceptions;
using FC4.HotelReservation.Application.UseCases.Reservation.Common;
using FC4.HotelReservation.Domain.Repositories;

namespace FC4.HotelReservation.Application.UseCases.Reservation.GetReservation;

public class GetReservation(IReservationRepository reservationRepository) : IGetReservation
{
    public async Task<ReservationOutput> Handle(GetReservationInput request, CancellationToken cancellationToken)
    {
        var reservation = await reservationRepository.GetByIdAsync(request.ReservationId, cancellationToken)
                          ?? throw new NotFoundException("Reservation not found");

        return ReservationOutput.FromReservation(reservation);
    }
}