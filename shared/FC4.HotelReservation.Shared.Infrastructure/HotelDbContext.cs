using FC4.HotelReservation.Catalog.Domain.Entities;
using FC4.HotelReservation.Guests.Domain.Entities;
using FC4.HotelReservation.Shared.Infrastructure.Mappings;
using FC4.HotelReservation.Payments.Domain.Entities;
using FC4.HotelReservation.Reservations.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FC4.HotelReservation.Shared.Infrastructure;

public class HotelDbContext(DbContextOptions<HotelDbContext> options) : DbContext(options)
{
    public DbSet<Guest> Guests { get; set; }
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<RoomType> RoomTypes { get; set; }
    public DbSet<RoomTypeInventory> RoomTypeInventories { get; set; }
    public DbSet<RoomTypeRate> RoomTypeRates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new GuestConfiguration());
        modelBuilder.ApplyConfiguration(new HotelConfiguration());
        modelBuilder.ApplyConfiguration(new PaymentConfiguration());
        modelBuilder.ApplyConfiguration(new ReservationConfiguration());
        modelBuilder.ApplyConfiguration(new RoomConfiguration());
        modelBuilder.ApplyConfiguration(new RoomTypeConfiguration());
        modelBuilder.ApplyConfiguration(new RoomTypeInventoryConfiguration());
        modelBuilder.ApplyConfiguration(new RoomTypeRateConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}