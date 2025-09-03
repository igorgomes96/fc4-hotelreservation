using FC4.HotelReservation.Payments.Domain.Enums;
using MediatR;

namespace FC4.HotelReservation.Payments.Application.UseCases.Payment.UpdatePaymentStatus;

public record UpdatePaymentStatusInput(
    Guid PaymentId,
    PaymentStatus Status,
    string TransactionId
) : IRequest;