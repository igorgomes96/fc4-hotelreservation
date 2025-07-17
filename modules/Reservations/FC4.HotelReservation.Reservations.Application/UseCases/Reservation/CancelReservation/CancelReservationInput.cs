using MediatR;

namespace FC4.HotelReservation.Reservations.Application.UseCases.Reservation.CancelReservation;

public record CancelReservationInput(Guid ReservationId) : IRequest;