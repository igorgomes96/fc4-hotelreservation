using FC4.HotelReservation.Domain.Entities;
using FC4.HotelReservation.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using static FC4.HotelReservation.IntegrationTests.DataBuilders.HotelBuilder;

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
        hotel ??= AHotel();
    
        using var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<HotelDbContext>();
    
        await dbContext.Hotels.AddAsync(hotel);
        await dbContext.SaveChangesAsync();
    
        return hotel;
    }
    
    public async Task<Domain.Entities.Hotel?> GetHotelByIdAsync(Guid hotelId)
    {
        using var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<HotelDbContext>();
        return await dbContext.Hotels.FirstOrDefaultAsync(h => h.Id == hotelId);
    }
    
    public async Task<Room?> GetRoomByIdAsync(Guid roomId)
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
    
    public async Task<Payment?> GetPaymentByIdAsync(Guid paymentId)
    {
        using var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<HotelDbContext>();
        return await dbContext.Payments.FirstOrDefaultAsync(p => p.Id == paymentId);
    }
}