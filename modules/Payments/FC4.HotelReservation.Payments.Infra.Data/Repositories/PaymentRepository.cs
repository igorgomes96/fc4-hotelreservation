using FC4.HotelReservation.Payments.Domain.Entities;
using FC4.HotelReservation.Payments.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FC4.HotelReservation.Payments.Infra.Data.Repositories;

public class PaymentRepository(PaymentsDbContext context) : IPaymentRepository
{
    public async Task<Payment?> GetByIdAsync(Guid paymentId, CancellationToken cancellationToken)
    {
        return await context.Payments
            .FirstOrDefaultAsync(p => p.Id == paymentId, cancellationToken);
    }

    public async Task CreateAsync(Payment payment, CancellationToken cancellationToken)
    {
        await context.Payments.AddAsync(payment, cancellationToken);
    }

    public Task UpdateAsync(Payment payment, CancellationToken cancellationToken)
    {
        context.Payments.Update(payment);
        return Task.CompletedTask;
    }
}