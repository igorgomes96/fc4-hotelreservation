using FC4.HotelReservation.Payments.Application.UseCases.Payment.UpdatePaymentStatus;
using FC4.HotelReservation.Payments.Domain.Enums;

namespace FC4.HotelReservation.WebApi.Models;

public record UpdatePaymentStatusRequest(
    PaymentStatus Status,
    string TransactionId)
{
    public UpdatePaymentStatusInput ToInput(Guid paymentId)
    {
        return new UpdatePaymentStatusInput(paymentId, Status, TransactionId);
    }
}