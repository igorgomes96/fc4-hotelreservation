using MediatR;

namespace FC4.HotelReservation.Reservations.Application.UseCases.Reservation.CreateReservation;

public interface ICreateReservation: IRequestHandler<CreateReservationInput, CreateReservationOutput>;