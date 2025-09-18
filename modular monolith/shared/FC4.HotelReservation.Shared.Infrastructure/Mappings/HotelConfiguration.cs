using FC4.HotelReservation.Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FC4.HotelReservation.Shared.Infrastructure.Mappings;

public class HotelConfiguration: IEntityTypeConfiguration<Hotel>
{
    public void Configure(EntityTypeBuilder<Hotel> builder)
    {
        builder.ToTable("hotels");
        
        builder.HasKey(h => h.Id);
        builder.Property(h => h.Id)
            .HasColumnName("id")
            .ValueGeneratedNever();
            
        builder.Property(h => h.Name)
            .HasColumnName("name")
            .HasMaxLength(200)
            .IsRequired();
            
        builder.OwnsOne(h => h.Address, address =>
        {
            address.Property(a => a.Street)
                .HasColumnName("street")
                .HasMaxLength(300)
                .IsRequired();
                
            address.Property(a => a.City)
                .HasColumnName("city")
                .HasMaxLength(100)
                .IsRequired();
                
            address.Property(a => a.State)
                .HasColumnName("state")
                .HasMaxLength(100)
                .IsRequired();
                
            address.Property(a => a.Country)
                .HasColumnName("country")
                .HasMaxLength(100)
                .IsRequired();
                
            address.Property(a => a.ZipCode)
                .HasColumnName("zip_code")
                .HasMaxLength(20)
                .IsRequired();
        });
        
        builder.Ignore(g => g.Events);
    }
}