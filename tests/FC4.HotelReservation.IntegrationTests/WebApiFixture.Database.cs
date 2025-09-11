using FC4.HotelReservation.Catalog.Domain.Entities;
using FC4.HotelReservation.Reservations.Domain.Entities;
using FC4.HotelReservation.Reservations.Domain.ValueObjects;
using FC4.HotelReservation.Shared.Infrastructure;
using FC4.HotelReservation.Shared.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using static FC4.HotelReservation.IntegrationTests.DataBuilders.HotelBuilder;
using static FC4.HotelReservation.IntegrationTests.DataBuilders.RoomBuilder;
using static FC4.HotelReservation.IntegrationTests.DataBuilders.RoomTypeBuilder;
using static FC4.HotelReservation.IntegrationTests.DataBuilders.ReservationBuilder;
using static FC4.HotelReservation.IntegrationTests.DataBuilders.PaymentBuilder;
using static FC4.HotelReservation.IntegrationTests.DataBuilders.GuestBuilder;
using static FC4.HotelReservation.IntegrationTests.DataBuilders.RoomTypeRateBuilder;
using static FC4.HotelReservation.IntegrationTests.DataBuilders.RoomTypeInventoryBuilder;

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
    
    private async Task<T> AddToDatabaseAsync<T>(T entity) where T : Entity
    {
        using var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<HotelDbContext>();

        await dbContext.Set<T>().AddAsync(entity);
        await dbContext.SaveChangesAsync();

        return entity;
    }

    public async Task<Catalog.Domain.Entities.Hotel> CreateHotelInDatabaseAsync(Catalog.Domain.Entities.Hotel? hotel = null)
    {
        hotel ??= AHotel().Build();
        return await AddToDatabaseAsync(hotel);
    }

    public async Task<Catalog.Domain.Entities.Room> CreateRoomInDatabaseAsync(Catalog.Domain.Entities.Room? room = null)
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
        return await AddToDatabaseAsync(room);
    }

    public async Task<RoomType> CreateRoomTypeInDatabaseAsync(RoomType? roomType = null)
    {
        roomType ??= ARoomType().Build();
        return await AddToDatabaseAsync(roomType);
    }

    public async Task<Reservations.Domain.Entities.Reservation> CreateReservationInDatabaseAsync(
        Reservations.Domain.Entities.Reservation? reservation = null)
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
        return await AddToDatabaseAsync(reservation);
    }

    public async Task<Payments.Domain.Entities.Payment> CreatePaymentInDatabaseAsync(Payments.Domain.Entities.Payment? payment = null)
    {
        payment ??= APayment().Build();
        return await AddToDatabaseAsync(payment);
    }

    public async Task<Guests.Domain.Entities.Guest> CreateGuestInDatabaseAsync(Guests.Domain.Entities.Guest? guest = null)
    {
        guest ??= AGuest().Build();
        return await AddToDatabaseAsync(guest);
    }

    public async Task<RoomTypeRate> CreateRoomTypeRateInDatabaseAsync(RoomTypeRate? rate = null)
    {
        rate ??= ARoomTypeRate().Build();
        return await AddToDatabaseAsync(rate);
    }

    public async Task<RoomTypeInventory> CreateRoomTypeInventoryInDatabaseAsync(RoomTypeInventory? inventory = null)
    {
        inventory ??= ARoomTypeInventory().Build();
        return await AddToDatabaseAsync(inventory);
    }

    public async Task<Payments.Domain.Entities.Payment?> GetPaymentByReservationIdAsync(Guid reservationId)
    {
        using var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<HotelDbContext>();
        return await dbContext.Payments.FirstOrDefaultAsync(p => p.ReservationId == reservationId);
    }

    public async Task<List<RoomTypeInventory>> GetRoomTypeInventoriesAsync(Guid hotelId, Guid roomTypeId,
        DateRange period)
    {
        using var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<HotelDbContext>();

        var dates = period.GetDates().ToList();
        return await dbContext.RoomTypeInventories
            .Where(i => i.HotelId == hotelId &&
                        i.RoomTypeId == roomTypeId &&
                        dates.Contains(i.Date))
            .ToListAsync();
    }


    public async Task<Catalog.Domain.Entities.Hotel?> GetHotelByIdAsync(Guid hotelId)
    {
        using var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<HotelDbContext>();
        return await dbContext.Hotels.FirstOrDefaultAsync(h => h.Id == hotelId);
    }

    public async Task<Catalog.Domain.Entities.Room?> GetRoomByIdAsync(Guid roomId)
    {
        using var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<HotelDbContext>();
        return await dbContext.Rooms.FirstOrDefaultAsync(r => r.Id == roomId);
    }
    
    public async Task<Guests.Domain.Entities.Guest?> GetGuestByIdAsync(Guid hotelId)
    {
        using var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<HotelDbContext>();
        return await dbContext.Guests.FirstOrDefaultAsync(h => h.Id == hotelId);
    }

    public async Task<Reservations.Domain.Entities.Reservation?> GetReservationByIdAsync(Guid reservationId)
    {
        using var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<HotelDbContext>();
        return await dbContext.Reservations.FirstOrDefaultAsync(r => r.Id == reservationId);
    }

    public async Task<Payments.Domain.Entities.Payment?> GetPaymentByIdAsync(Guid paymentId)
    {
        using var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<HotelDbContext>();
        return await dbContext.Payments.FirstOrDefaultAsync(p => p.Id == paymentId);
    }

    public async Task<List<Reservations.Domain.Entities.Reservation>> GetReservationsByGuestIdAsync(Guid guestId)
    {
        using var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<HotelDbContext>();
        return await dbContext.Reservations
            .Where(r => r.GuestId == guestId)
            .ToListAsync();
    }

    public async Task<RoomTypeInventory?> GetRoomTypeInventoryAsync(Guid hotelId, Guid roomTypeId, DateTime startDate)
    {
        using var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<HotelDbContext>();

        return await dbContext.RoomTypeInventories
            .FirstOrDefaultAsync(i => i.HotelId == hotelId &&
                                      i.RoomTypeId == roomTypeId &&
                                      i.Date == startDate);
    }
}