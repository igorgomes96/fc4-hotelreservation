using FC4.HotelReservation.Payments.Domain.Repositories;
using FC4.HotelReservation.Shared.Application;
using FC4.HotelReservation.Shared.Domain.ValueObjects;
using Entities = FC4.HotelReservation.Payments.Domain.Entities;

namespace FC.HotelReservation.Payments.Application.UseCases.Payment.CreatePendingPayment;

public class CreatePendingPayment(
    IUnitOfWork unitOfWork,
    IPaymentRepository paymentRepository) : ICreatePendingPayment
{
    public async Task Handle(CreatePendingPaymentInput request, CancellationToken cancellationToken)
    {
        var payment = new Entities.Payment(request.ReservationId, new Money(request.Amount, request.Currency));
        await paymentRepository.CreateAsync(payment, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
    }
}