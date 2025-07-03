using MediatR;

namespace FC4.HotelReservation.Application.UseCases.Reservation.CancelReservation;

public interface ICancelReservation: IRequestHandler<CancelReservationInput>;