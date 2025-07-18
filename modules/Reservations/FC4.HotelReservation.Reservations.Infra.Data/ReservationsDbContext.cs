using FC4.HotelReservation.Reservations.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FC4.HotelReservation.Reservations.Infra.Data;

public class ReservationsDbContext(DbContextOptions<ReservationsDbContext> options) : DbContext(options)
{
    public DbSet<Guest> Guests { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<RoomTypeInventory> RoomTypeInventories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("reservations");
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReservationsDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}