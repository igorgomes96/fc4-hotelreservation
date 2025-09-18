using MediatR;

namespace FC4.HotelReservation.Reservations.Application.UseCases.Reservation.ProcessPaymentStatus;

public interface IProcessPaymentStatus : IRequestHandler<ProcessPaymentStatusInput>;