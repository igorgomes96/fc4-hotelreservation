using MediatR;

namespace FC.HotelReservation.Payments.Application.UseCases.Payment.UpdatePaymentStatus;

public interface IUpdatePaymentStatus : IRequestHandler<UpdatePaymentStatusInput>;