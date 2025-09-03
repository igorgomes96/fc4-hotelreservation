using MediatR;

namespace FC4.HotelReservation.Payments.Application.UseCases.Payment.CreatePendingPayment;

public record CreatePendingPaymentInput(Guid ReservationId, decimal Amount, string Currency) : IRequest;