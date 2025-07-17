using MediatR;

namespace FC.HotelReservation.Payments.Application.UseCases.Payment.CreatePendingPayment;

public interface ICreatePendingPayment : IRequestHandler<CreatePendingPaymentInput>
{
    
}