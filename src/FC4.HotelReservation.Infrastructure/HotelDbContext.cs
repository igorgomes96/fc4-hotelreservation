using FC4.HotelReservation.Payments.Domain.Entities;
using FC4.HotelReservation.Reservations.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FC4.HotelReservation.Infrastructure;

public class HotelDbContext(DbContextOptions<HotelDbContext> options) : DbContext(options)
{
    public DbSet<Guest> Guests { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<RoomTypeInventory> RoomTypeInventories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HotelDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}