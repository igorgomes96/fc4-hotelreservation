using FC4.HotelReservation.Application.UseCases.Reservation.Common;
using MediatR;

namespace FC4.HotelReservation.Application.UseCases.Reservation.ListReservations;

public interface IListReservations : IRequestHandler<ListReservationsInput, IEnumerable<ReservationOutput>>;