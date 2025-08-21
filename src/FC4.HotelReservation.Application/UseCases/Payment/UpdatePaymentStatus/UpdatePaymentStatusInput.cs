using FC4.HotelReservation.Domain.Enums;
using FC4.HotelReservation.Payments.Domain.Enums;
using MediatR;

namespace FC4.HotelReservation.Application.UseCases.Payment.UpdatePaymentStatus;

public record UpdatePaymentStatusInput(
    Guid PaymentId,
    PaymentStatus Status,
    string TransactionId
) : IRequest;