using System.Net;
using System.Net.Http.Json;
using FC4.HotelReservation.Payments.Domain.Enums;
using FC4.HotelReservation.Reservations.Domain.Enums;
using FC4.HotelReservation.Reservations.Domain.ValueObjects;
using FluentAssertions;
using static FC4.HotelReservation.IntegrationTests.DataBuilders.UpdatePaymentStatusInputBuilder;
using static FC4.HotelReservation.IntegrationTests.DataBuilders.PaymentBuilder;
using static FC4.HotelReservation.IntegrationTests.DataBuilders.RoomTypeInventoryBuilder;
using static FC4.HotelReservation.IntegrationTests.DataBuilders.ReservationBuilder;

namespace FC4.HotelReservation.IntegrationTests.Payment;

[Collection(nameof(WebApiFixture))]
public class UpdatePaymentStatusTest(WebApiFixture fixture) : IAsyncDisposable
{
    private readonly HttpClient _client = fixture.CreateClient();

    [Theory]
    [InlineData(PaymentStatus.Processing, PaymentStatus.Completed, ReservationStatus.Paid)]
    [InlineData(PaymentStatus.Processing, PaymentStatus.Failed, ReservationStatus.Rejected)]
    [InlineData(PaymentStatus.Completed, PaymentStatus.Refunded, ReservationStatus.Cancelled)]
    public async Task UpdatePaymentStatus_WithStatusThatChangesReservation_ShouldUpdateReservationStatus(
        PaymentStatus currentPaymentStatus,
        PaymentStatus newPaymentStatus,
        ReservationStatus expectedReservationStatus)
    {
        // Arrange
        var reservation = await fixture.CreateReservationInDatabaseAsync();
        var payment = APayment()
            .WithStatus(currentPaymentStatus)
            .WithReservationId(reservation.Id).Build();
        await fixture.CreatePaymentInDatabaseAsync(payment);

        var input = AUpdatePaymentStatusInput()
            .WithPaymentId(payment.Id)
            .WithStatus(newPaymentStatus)
            .Build();

        // Act
        var response = await _client.PatchAsJsonAsync($"/v1/payments/{payment.Id}", input);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var updatedPayment = await fixture.GetPaymentByIdAsync(payment.Id);
        updatedPayment.Should().NotBeNull();
        updatedPayment.Status.Should().Be(newPaymentStatus);

        await Task.Delay(1000);
        var updatedReservation = await fixture.GetReservationByIdAsync(reservation.Id);
        updatedReservation.Should().NotBeNull();
        updatedReservation.Status.Should().Be(expectedReservationStatus);
    }

    [Fact]
    public async Task UpdatePaymentStatus_WhenPaymentFailed_ShouldUpdateInventory()
    {
        // Arrange
        var hotel = await fixture.CreateHotelInDatabaseAsync();
        var guest = await fixture.CreateGuestInDatabaseAsync();
        var roomType = await fixture.CreateRoomTypeInDatabaseAsync();
        var startDate = DateTime.Today.AddDays(1);
        var endDate = startDate.AddDays(3);
        var period = new DateRange(startDate, endDate);
        const int roomQuantity = 2;
        const int currentReservedRooms = 5;

        // Create inventory and rate for each day in the period
        var dates = new DateRange(startDate, endDate).GetDates().ToList();
        foreach (var date in dates)
        {
            await fixture.CreateRoomTypeInventoryInDatabaseAsync(
                ARoomTypeInventory()
                    .WithHotelId(hotel.Id)
                    .WithRoomTypeId(roomType.Id)
                    .WithDate(date)
                    .WithTotalInventory(10)
                    .WithTotalReserved(currentReservedRooms)
                    .Build());
        }

        var reservation = await fixture.CreateReservationInDatabaseAsync(
            AReservation()
                .WithGuestId(guest.Id)
                .WithHotelId(hotel.Id)
                .WithRoomTypeId(roomType.Id)
                .WithStartDate(startDate)
                .WithEndDate(endDate)
                .WithRoomQuantity(roomQuantity)
                .Build());

        var payment = await fixture.CreatePaymentInDatabaseAsync(
            APayment()
                .WithStatus(PaymentStatus.Processing)
                .WithReservationId(reservation.Id)
                .Build());

        var input = AUpdatePaymentStatusInput()
            .WithPaymentId(payment.Id)
            .WithStatus(PaymentStatus.Failed)
            .Build();

        // Act
        var response = await _client.PatchAsJsonAsync($"/v1/payments/{payment.Id}", input);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var updatedPayment = await fixture.GetPaymentByIdAsync(payment.Id);
        updatedPayment.Should().NotBeNull();
        updatedPayment.Status.Should().Be(PaymentStatus.Failed);

        await Task.Delay(1000);
        var updatedReservation = await fixture.GetReservationByIdAsync(reservation.Id);
        updatedReservation.Should().NotBeNull();
        updatedReservation.Status.Should().Be(ReservationStatus.Rejected);

        var updatedInventories = await fixture.GetRoomTypeInventoriesAsync(hotel.Id, roomType.Id, period);
        updatedInventories.Should().HaveCount(dates.Count);
        updatedInventories.Should().OnlyContain(i => i.TotalReserved == currentReservedRooms - roomQuantity);
    }

    [Fact]
    public async Task UpdatePaymentStatus_WithProcessingStatus_ShouldNotUpdateReservationStatus()
    {
        // Arrange
        var reservation = await fixture.CreateReservationInDatabaseAsync();
        var payment = await fixture.CreatePaymentInDatabaseAsync(
            APayment().WithReservationId(reservation.Id).Build());

        var input = AUpdatePaymentStatusInput()
            .WithPaymentId(payment.Id)
            .WithStatus(PaymentStatus.Processing)
            .Build();

        // Act
        var response = await _client.PatchAsJsonAsync($"/v1/payments/{payment.Id}", input);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var updatedPayment = await fixture.GetPaymentByIdAsync(payment.Id);
        updatedPayment.Should().NotBeNull();
        updatedPayment.Status.Should().Be(input.Status);

        await Task.Delay(1000);
        var updatedReservation = await fixture.GetReservationByIdAsync(reservation.Id);
        updatedReservation.Should().NotBeNull();
        updatedReservation.Status.Should().Be(reservation.Status);
    }

    [Fact]
    public async Task UpdatePaymentStatus_WithNonExistentPayment_ShouldReturnNotFound()
    {
        // Arrange
        var nonExistentId = Guid.NewGuid();
        var input = AUpdatePaymentStatusInput().WithPaymentId(nonExistentId).Build();

        // Act
        var response = await _client.PatchAsJsonAsync($"/v1/payments/{nonExistentId}", input);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    public async ValueTask DisposeAsync()
    {
        await fixture.CleanDatabaseAsync();
    }
}