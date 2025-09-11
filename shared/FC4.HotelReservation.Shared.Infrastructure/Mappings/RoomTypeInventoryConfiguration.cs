using FC4.HotelReservation.Catalog.Domain.Entities;
using FC4.HotelReservation.Reservations.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FC4.HotelReservation.Shared.Infrastructure.Mappings;

public class RoomTypeInventoryConfiguration : IEntityTypeConfiguration<RoomTypeInventory>
{
    public void Configure(EntityTypeBuilder<RoomTypeInventory> builder)
    {
        builder.ToTable("room_type_inventory");
        
        builder.HasKey(rti => rti.Id);
        builder.Property(rti => rti.Id)
            .HasColumnName("id")
            .ValueGeneratedNever();
            
        builder.Property(rti => rti.HotelId)
            .HasColumnName("hotel_id")
            .IsRequired();
            
        builder.Property(rti => rti.RoomTypeId)
            .HasColumnName("room_type_id")
            .IsRequired();
            
        builder.Property(rti => rti.Date)
            .HasColumnName("date")
            .HasConversion(date => DateTime.SpecifyKind(date, DateTimeKind.Utc),
                date => DateTime.SpecifyKind(date, DateTimeKind.Utc))
            .IsRequired();
            
        builder.Property(rti => rti.TotalInventory)
            .HasColumnName("total_inventory")
            .IsRequired();
            
        builder.Property(rti => rti.TotalReserved)
            .HasColumnName("total_reserved")
            .IsRequired();
            
        builder.HasOne<Hotel>()
            .WithMany()
            .HasForeignKey(rti => rti.HotelId)
            .HasConstraintName("fk_room_type_inventories_hotels");
            
        builder.HasOne<RoomType>()
            .WithMany()
            .HasForeignKey(rti => rti.RoomTypeId)
            .HasConstraintName("fk_room_type_inventories_room_types");
        
        builder.HasIndex(rti => new { rti.HotelId, rti.RoomTypeId, rti.Date })
            .IsUnique();
        
        builder.Ignore(g => g.Events);
    }
}