using FC4.HotelReservation.Domain.Entities;
using FC4.HotelReservation.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;

namespace FC4.HotelReservation.Infrastructure;

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
    /*
    public override int SaveChanges()
    {
        ConvertDateTimesToUtc();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ConvertDateTimesToUtc();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void ConvertDateTimesToUtc()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            foreach (var prop in entry.Properties)
            {
                if (prop.CurrentValue is DateTime dt && dt.Kind != DateTimeKind.Utc)
                {
                    prop.CurrentValue = DateTime.SpecifyKind(dt, DateTimeKind.Utc);
                }
            }
        }
    }*/
}