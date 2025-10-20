using FC4.HotelReservation.Reservations.Domain.Entities;
using FC4.HotelReservation.Shared.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;

namespace FC4.HotelReservation.Shared.Infrastructure;

public class HotelDbContext(DbContextOptions<HotelDbContext> options) : DbContext(options)
{
    public DbSet<Reservation> Reservations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ReservationConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}