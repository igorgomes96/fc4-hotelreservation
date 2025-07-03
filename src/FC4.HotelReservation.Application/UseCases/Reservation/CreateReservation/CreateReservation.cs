using FC4.HotelReservation.Domain.Repositories;

namespace FC4.HotelReservation.Application.UseCases.Reservation.CreateReservation;

public class CreateReservation(IReservationRepository reservationRepository) : ICreateReservation
{
    public async Task<CreateReservationOutput> Handle(
        CreateReservationInput request,
        CancellationToken cancellationToken)
    {
        var reservation = request.ToReservation();
        await reservationRepository.CreateAsync(reservation, cancellationToken);
        return new CreateReservationOutput(reservation.Id);
    }
}