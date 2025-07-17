using FC4.HotelReservation.Application.Common;
using FC4.HotelReservation.Application.Exceptions;
using FC4.HotelReservation.Payments.Domain.Enums;
using FC4.HotelReservation.Payments.Domain.Repositories;

namespace FC.HotelReservation.Payments.Application.UseCases.Payment.UpdatePaymentStatus;

public class UpdatePaymentStatus(IPaymentRepository paymentRepository, IUnitOfWork unitOfWork) : IUpdatePaymentStatus
{
    public async Task Handle(UpdatePaymentStatusInput request, CancellationToken cancellationToken)
    {
        var payment = await paymentRepository.GetByIdAsync(request.PaymentId, cancellationToken)
                      ?? throw new NotFoundException("Payment not found");

        switch (request.Status)
        {
            case PaymentStatus.Processing:
                payment.MarkAsProcessing(request.TransactionId);
                break;
            case PaymentStatus.Completed:
                payment.MarkAsCompleted();
                break;
            case PaymentStatus.Failed:
                payment.MarkAsFailed();
                break;
            case PaymentStatus.Refunded:
                payment.Refund();
                break;
            case PaymentStatus.Pending:
            default:
                throw new InvalidOperationException($"Cannot update payment to status {request.Status}");
        }

        await paymentRepository.UpdateAsync(payment, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
    }
}