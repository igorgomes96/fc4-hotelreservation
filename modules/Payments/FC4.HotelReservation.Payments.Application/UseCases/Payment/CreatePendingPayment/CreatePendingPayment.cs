using FC4.HotelReservation.Payments.Domain.Repositories;
using FC4.HotelReservation.Payments.Domain.ValueObjects;
using FC4.HotelReservation.Shared.Application;

namespace FC4.HotelReservation.Payments.Application.UseCases.Payment.CreatePendingPayment;

public class CreatePendingPayment(
    IPaymentRepository paymentRepository,
    IUnitOfWork unitOfWork
    ) : ICreatePendingPayment
{
    public async Task Handle(CreatePendingPaymentInput request, CancellationToken cancellationToken)
    {
        var payment = Domain.Entities.Payment.Create(request.ReservationId,
            new Money(request.Amount, request.Currency));
        await paymentRepository.CreateAsync(payment, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
    }
}