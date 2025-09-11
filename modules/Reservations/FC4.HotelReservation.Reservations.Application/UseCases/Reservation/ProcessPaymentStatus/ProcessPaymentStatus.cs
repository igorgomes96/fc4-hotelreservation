using FC4.HotelReservation.Reservations.Domain.Repositories;
using FC4.HotelReservation.Shared.Application;

namespace FC4.HotelReservation.Reservations.Application.UseCases.Reservation.ProcessPaymentStatus;

public class ProcessPaymentStatus(
    IReservationRepository reservationRepository,
    IUnitOfWork unitOfWork): IProcessPaymentStatus
{
    public async Task Handle(ProcessPaymentStatusInput request, CancellationToken cancellationToken)
    {
        var reservation = await reservationRepository.GetByIdAsync(request.ReservationId, cancellationToken)
                          ?? throw new InvalidOperationException("Reservation not found");

        switch (request.PaymentStatus)
        {
            case PaymentStatus.Completed:
                reservation.MarkAsPaid();
                break;
            case PaymentStatus.Failed:
                reservation.Reject();
                break;
            case PaymentStatus.Refunded:
                reservation.Cancel();
                break;
            case PaymentStatus.Pending:
            case PaymentStatus.Processing:
            default:
                return;
        }

        await reservationRepository.UpdateAsync(reservation, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
    }
}