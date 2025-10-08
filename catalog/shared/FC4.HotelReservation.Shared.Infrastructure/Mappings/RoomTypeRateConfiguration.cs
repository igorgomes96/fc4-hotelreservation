using FC4.HotelReservation.Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FC4.HotelReservation.Shared.Infrastructure.Mappings;

public class RoomTypeRateConfiguration : IEntityTypeConfiguration<RoomTypeRate>
{
    public void Configure(EntityTypeBuilder<RoomTypeRate> builder)
    {
        builder.ToTable("room_type_rates");
        
        builder.HasKey(rtr => rtr.Id);
        builder.Property(rtr => rtr.Id)
            .HasColumnName("id")
            .ValueGeneratedNever();
            
        builder.Property(rtr => rtr.HotelId)
            .HasColumnName("hotel_id")
            .IsRequired();
            
        builder.Property(rtr => rtr.RoomTypeId)
            .HasColumnName("room_type_id")
            .IsRequired();
            
        builder.Property(rtr => rtr.Date)
            .HasColumnName("date")
            .HasConversion(date => DateTime.SpecifyKind(date, DateTimeKind.Utc),
                date => DateTime.SpecifyKind(date, DateTimeKind.Utc))
            .IsRequired();
            
        builder.OwnsOne(rtr => rtr.Rate, money =>
        {
            money.Property(m => m.Value)
                .HasColumnName("rate_amount")
                .HasPrecision(18, 2)
                .IsRequired();
                
            money.Property(m => m.Currency)
                .HasColumnName("rate_currency")
                .HasMaxLength(3)
                .IsRequired();
        });
        
        builder.HasOne<Hotel>()
            .WithMany()
            .HasForeignKey(rtr => rtr.HotelId)
            .HasConstraintName("fk_room_type_rates_hotels");
            
        builder.HasOne<RoomType>()
            .WithMany()
            .HasForeignKey(rtr => rtr.RoomTypeId)
            .HasConstraintName("fk_room_type_rates_room_types");
        
        builder.HasIndex(rtr => new { rtr.HotelId, rtr.RoomTypeId, rtr.Date })
            .IsUnique();
        
        builder.Ignore(g => g.Events);
    }
}