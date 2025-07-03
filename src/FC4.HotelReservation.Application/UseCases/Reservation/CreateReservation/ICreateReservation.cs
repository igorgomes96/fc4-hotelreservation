using MediatR;

namespace FC4.HotelReservation.Application.UseCases.Reservation.CreateReservation;

public interface ICreateReservation: IRequestHandler<CreateReservationInput, CreateReservationOutput>;