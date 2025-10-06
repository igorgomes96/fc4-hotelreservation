using FC4.HotelReservation.Application.Services;
using FC4.HotelReservation.Domain.Enums;

namespace FC4.HotelReservation.Infrastructure.Gateways;

public class ExternalReservationsGateway : IReservationsGateway
{
    public async Task ProcessPaymentStatusChangedAsync(
        Guid reservationId,
        Guid paymentId,
        PaymentStatus paymentStatus,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
        // var httpClient = new HttpClient();
        // var content = new
        // {
        //     PaymentId = paymentId,
        //     PaymentStatus = paymentStatus.ToString()
        // };
        // await httpClient.PatchAsync($"/api/reservations/{reservationId}/payment",
        //     new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json"),
        //     cancellationToken);
    }
}