using MediatR;

namespace FC4.HotelReservation.Payments.Application.UseCases.Payment.UpdatePaymentStatus;

public interface IUpdatePaymentStatus : IRequestHandler<UpdatePaymentStatusInput>;