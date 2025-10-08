using FC4.HotelReservation.Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FC4.HotelReservation.Shared.Infrastructure.Mappings;

public class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.ToTable("rooms");
        
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id)
            .HasColumnName("id")
            .ValueGeneratedNever();
            
        builder.Property(r => r.HotelId)
            .HasColumnName("hotel_id")
            .IsRequired();
            
        builder.Property(r => r.RoomTypeId)
            .HasColumnName("room_type_id")
            .IsRequired();
            
        builder.Property(r => r.Floor)
            .HasColumnName("floor")
            .IsRequired();
            
        builder.Property(r => r.Number)
            .HasColumnName("number")
            .HasMaxLength(10)
            .IsRequired();
            
        builder.Property(r => r.IsAvailable)
            .HasColumnName("is_available")
            .IsRequired();
            
        builder.HasOne<Hotel>()
            .WithMany()
            .HasForeignKey(r => r.HotelId)
            .HasConstraintName("fk_rooms_hotels");
            
        builder.HasOne<RoomType>()
            .WithMany()
            .HasForeignKey(r => r.RoomTypeId)
            .HasConstraintName("fk_rooms_room_types");
        
        builder.Ignore(g => g.Events);
    }
}