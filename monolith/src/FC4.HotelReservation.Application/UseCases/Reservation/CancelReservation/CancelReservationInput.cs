using MediatR;

namespace FC4.HotelReservation.Application.UseCases.Reservation.CancelReservation;

public record CancelReservationInput(Guid ReservationId) : IRequest;