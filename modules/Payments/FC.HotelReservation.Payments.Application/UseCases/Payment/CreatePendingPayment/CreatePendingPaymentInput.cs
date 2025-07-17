using MediatR;

namespace FC.HotelReservation.Payments.Application.UseCases.Payment.CreatePendingPayment;

public record CreatePendingPaymentInput(Guid ReservationId, decimal Amount, string Currency)
    : IRequest;