using FC4.HotelReservation.Catalog.Domain.Entities;
using FC4.HotelReservation.Guests.Domain.Entities;
using FC4.HotelReservation.Reservations.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FC4.HotelReservation.Shared.Infrastructure.Mappings;

public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.ToTable("reservations");
        
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
            
        builder.Property(r => r.Status)
            .HasColumnName("status")
            .HasConversion<string>()
            .IsRequired();
            
        builder.Property(r => r.GuestId)
            .HasColumnName("guest_id")
            .IsRequired();
            
        builder.Property(r => r.RoomQuantity)
            .HasColumnName("room_quantity")
            .IsRequired();
            
        builder.Property(r => r.CreatedAt)
            .HasColumnName("created_at")
            .HasConversion(date => DateTime.SpecifyKind(date, DateTimeKind.Utc),
                date => DateTime.SpecifyKind(date, DateTimeKind.Utc))
            .IsRequired();
        
        builder.OwnsOne(r => r.StayPeriod, period =>
        {
            period.Property(p => p.StartDate)
                .HasColumnName("stay_start_date")
                .HasConversion(date => DateTime.SpecifyKind(date, DateTimeKind.Utc),
                    date => DateTime.SpecifyKind(date, DateTimeKind.Utc))
                .IsRequired();
                
            period.Property(p => p.EndDate)
                .HasColumnName("stay_end_date")
                .HasConversion(date => DateTime.SpecifyKind(date, DateTimeKind.Utc),
                    date => DateTime.SpecifyKind(date, DateTimeKind.Utc))
                .IsRequired();
        });

        builder.OwnsOne(r => r.TotalAmount, money =>
        {
            money.Property(m => m.Value)
                .HasColumnName("total_amount")
                .HasPrecision(18, 2)
                .IsRequired();
                
            money.Property(m => m.Currency)
                .HasColumnName("total_currency")
                .HasMaxLength(3)
                .IsRequired();
        });
        
        builder.HasOne<Hotel>()
            .WithMany()
            .HasForeignKey(r => r.HotelId)
            .HasConstraintName("fk_reservations_hotels");
            
        builder.HasOne<RoomType>()
            .WithMany()
            .HasForeignKey(r => r.RoomTypeId)
            .HasConstraintName("fk_reservations_room_types");
            
        builder.HasOne<Guest>()
            .WithMany()
            .HasForeignKey(r => r.GuestId)
            .HasConstraintName("fk_reservations_guests");
        
        builder.Ignore(g => g.Events);
    }
}
