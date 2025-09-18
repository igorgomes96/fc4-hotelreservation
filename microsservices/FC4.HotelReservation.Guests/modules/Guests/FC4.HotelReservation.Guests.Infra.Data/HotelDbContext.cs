using FC4.HotelReservation.Guests.Domain.Entities;
using FC4.HotelReservation.Guests.Infra.Mappings;
using Microsoft.EntityFrameworkCore;

namespace FC4.HotelReservation.Guests.Infra;

public class HotelDbContext(DbContextOptions<HotelDbContext> options) : DbContext(options)
{
    public DbSet<Guest> Guests { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new GuestConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}