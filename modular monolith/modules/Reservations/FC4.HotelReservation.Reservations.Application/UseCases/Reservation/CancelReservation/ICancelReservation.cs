using MediatR;

namespace FC4.HotelReservation.Reservations.Application.UseCases.Reservation.CancelReservation;

public interface ICancelReservation: IRequestHandler<CancelReservationInput>;