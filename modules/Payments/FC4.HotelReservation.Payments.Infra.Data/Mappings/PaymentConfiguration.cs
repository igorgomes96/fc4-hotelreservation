using FC4.HotelReservation.Payments.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FC4.HotelReservation.Payments.Infra.Data.Mappings;

public class PaymentConfiguration: IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("payments");
        
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .HasColumnName("id")
            .ValueGeneratedNever();
            
        builder.Property(p => p.ReservationId)
            .HasColumnName("reservation_id")
            .IsRequired();
            
        builder.Property(p => p.Status)
            .HasColumnName("status")
            .HasConversion<string>()
            .IsRequired();
            
        builder.Property(p => p.ProcessedAt)
            .HasColumnName("processed_at")
            .IsRequired();
            
        builder.Property(p => p.TransactionId)
            .HasColumnName("transaction_id")
            .HasMaxLength(100);
            
        builder.OwnsOne(p => p.Amount, money =>
        {
            money.Property(m => m.Value)
                .HasColumnName("amount")
                .HasPrecision(18, 2)
                .IsRequired();
                
            money.Property(m => m.Currency)
                .HasColumnName("currency")
                .HasMaxLength(3)
                .IsRequired();
        });
        
        builder.Ignore(g => g.Events);
    }
}
