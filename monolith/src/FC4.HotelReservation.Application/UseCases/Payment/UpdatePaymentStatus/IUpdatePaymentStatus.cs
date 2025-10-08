using MediatR;

namespace FC4.HotelReservation.Application.UseCases.Payment.UpdatePaymentStatus;

public interface IUpdatePaymentStatus : IRequestHandler<UpdatePaymentStatusInput>;