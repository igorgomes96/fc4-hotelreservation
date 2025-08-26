using FC4.HotelReservation.Reservations.Application.UseCases.Reservation.Common;
using MediatR;

namespace FC4.HotelReservation.Reservations.Application.UseCases.Reservation.ListReservations;

public record ListReservationsInput(Guid GuestId) : IRequest<IEnumerable<ReservationOutput>>;