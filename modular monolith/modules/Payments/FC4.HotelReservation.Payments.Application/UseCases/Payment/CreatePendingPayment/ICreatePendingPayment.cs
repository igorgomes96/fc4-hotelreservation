using MediatR;

namespace FC4.HotelReservation.Payments.Application.UseCases.Payment.CreatePendingPayment;

public interface ICreatePendingPayment : IRequestHandler<CreatePendingPaymentInput>;