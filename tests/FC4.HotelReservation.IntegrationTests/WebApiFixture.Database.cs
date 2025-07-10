using FC4.HotelReservation.Domain.Entities;
using FC4.HotelReservation.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using static FC4.HotelReservation.IntegrationTests.DataBuilders.HotelBuilder;
using static FC4.HotelReservation.IntegrationTests.DataBuilders.RoomBuilder;
using static FC4.HotelReservation.IntegrationTests.DataBuilders.RoomTypeBuilder;
using static FC4.HotelReservation.IntegrationTests.DataBuilders.ReservationBuilder;
using static FC4.HotelReservation.IntegrationTests.DataBuilders.PaymentBuilder;
using static FC4.HotelReservation.IntegrationTests.DataBuilders.GuestBuilder;

namespace FC4.HotelReservation.IntegrationTests;

public partial class WebApiFixture
{
    public async Task CleanDatabaseAsync()
    {
        using var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<HotelDbContext>();

        await dbContext.Database.ExecuteSqlRawAsync("DELETE FROM payments");
        await dbContext.Database.ExecuteSqlRawAsync("DELETE FROM room_type_rates");
        await dbContext.Database.ExecuteSqlRawAsync("DELETE FROM room_type_inventories");
        await dbContext.Database.ExecuteSqlRawAsync("DELETE FROM reservations");
        await dbContext.Database.ExecuteSqlRawAsync("DELETE FROM rooms");
        await dbContext.Database.ExecuteSqlRawAsync("DELETE FROM room_types");
        await dbContext.Database.ExecuteSqlRawAsync("DELETE FROM hotels");
        await dbContext.Database.ExecuteSqlRawAsync("DELETE FROM guests");
    }

    public async Task<Domain.Entities.Hotel> CreateHotelInDatabaseAsync(Domain.Entities.Hotel? hotel = null)
    {
        hotel ??= AHotel().Build();

        using var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<HotelDbContext>();

        await dbContext.Hotels.AddAsync(hotel);
        await dbContext.SaveChangesAsync();

        return hotel;
    }

    public async Task<Domain.Entities.Room> CreateRoomInDatabaseAsync(Domain.Entities.Room? room = null)
    {
        if (room is null)
        {
            var hotel = await CreateHotelInDatabaseAsync();
            var roomType = await CreateRoomTypeInDatabaseAsync();
            room = ARoom()
                .WithRoomTypeId(roomType.Id)
                .WithHotelId(hotel.Id)
                .Build();
        }

        using var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<HotelDbContext>();

        await dbContext.Rooms.AddAsync(room);
        await dbContext.SaveChangesAsync();

        return room;
    }

    public async Task<RoomType> CreateRoomTypeInDatabaseAsync(RoomType? roomType = null)
    {
        roomType ??= ARoomType().Build();

        using var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<HotelDbContext>();

        await dbContext.RoomTypes.AddAsync(roomType);
        await dbContext.SaveChangesAsync();

        return roomType;
    }

    public async Task<Reservation> CreateReservationInDatabaseAsync(Reservation? reservation = null)
    {
        if (reservation is null)
        {
            var hotel = await CreateHotelInDatabaseAsync();
            var roomType = await CreateRoomTypeInDatabaseAsync();
            var guest = await CreateGuestInDatabaseAsync();
            reservation = AReservation()
                .WithHotelId(hotel.Id)
                .WithRoomTypeId(roomType.Id)
                .WithGuestId(guest.Id)
                .Build();
        }

        using var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<HotelDbContext>();

        await dbContext.Reservations.AddAsync(reservation);
        await dbContext.SaveChangesAsync();

        return reservation;
    }

    public async Task<Domain.Entities.Payment> CreatePaymentInDatabaseAsync(Domain.Entities.Payment? payment = null)
    {
        payment ??= APayment().Build();

        using var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<HotelDbContext>();

        await dbContext.Payments.AddAsync(payment);
        await dbContext.SaveChangesAsync();

        return payment;
    }

    public async Task<Guest> CreateGuestInDatabaseAsync(Guest? guest = null)
    {
        guest ??= AGuest().Build();
        
        using var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<HotelDbContext>();

        await dbContext.Guests.AddAsync(guest);
        await dbContext.SaveChangesAsync();

        return guest;
    }

    public async Task<Domain.Entities.Hotel?> GetHotelByIdAsync(Guid hotelId)
    {
        using var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<HotelDbContext>();
        return await dbContext.Hotels.FirstOrDefaultAsync(h => h.Id == hotelId);
    }

    public async Task<Domain.Entities.Room?> GetRoomByIdAsync(Guid roomId)
    {
        using var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<HotelDbContext>();
        return await dbContext.Rooms.FirstOrDefaultAsync(r => r.Id == roomId);
    }

    public async Task<Reservation?> GetReservationByIdAsync(Guid reservationId)
    {
        using var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<HotelDbContext>();
        return await dbContext.Reservations.FirstOrDefaultAsync(r => r.Id == reservationId);
    }

    public async Task<Domain.Entities.Payment?> GetPaymentByIdAsync(Guid paymentId)
    {
        using var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<HotelDbContext>();
        return await dbContext.Payments.FirstOrDefaultAsync(p => p.Id == paymentId);
    }
}