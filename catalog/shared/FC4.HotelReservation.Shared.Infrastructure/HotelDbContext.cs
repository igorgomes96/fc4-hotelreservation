using FC4.HotelReservation.Catalog.Domain.Entities;
using FC4.HotelReservation.Shared.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;

namespace FC4.HotelReservation.Shared.Infrastructure;

public class HotelDbContext(DbContextOptions<HotelDbContext> options) : DbContext(options)
{
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<RoomType> RoomTypes { get; set; }
    public DbSet<RoomTypeRate> RoomTypeRates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new HotelConfiguration());
        modelBuilder.ApplyConfiguration(new RoomConfiguration());
        modelBuilder.ApplyConfiguration(new RoomTypeConfiguration());
        modelBuilder.ApplyConfiguration(new RoomTypeRateConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}