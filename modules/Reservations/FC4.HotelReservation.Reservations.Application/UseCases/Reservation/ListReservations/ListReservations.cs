using FC4.HotelReservation.Domain.Repositories;
using FC4.HotelReservation.Reservations.Application.UseCases.Reservation.Common;

namespace FC4.HotelReservation.Reservations.Application.UseCases.Reservation.ListReservations;

public class ListReservations(IReservationRepository reservationRepository) : IListReservations
{
    public async Task<IEnumerable<ReservationOutput>> Handle(
        ListReservationsInput request,
        CancellationToken cancellationToken)
    {
        var reservations = await reservationRepository.GetByGuestIdAsync(request.GuestId, cancellationToken);
        return reservations.Select(ReservationOutput.FromReservation);
    }
}