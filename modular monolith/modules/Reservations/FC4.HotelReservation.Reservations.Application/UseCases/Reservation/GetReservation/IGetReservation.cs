using FC4.HotelReservation.Reservations.Application.UseCases.Reservation.Common;
using MediatR;

namespace FC4.HotelReservation.Reservations.Application.UseCases.Reservation.GetReservation;

public interface IGetReservation: IRequestHandler<GetReservationInput, ReservationOutput>;