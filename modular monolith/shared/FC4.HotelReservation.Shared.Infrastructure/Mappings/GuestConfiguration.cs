using FC4.HotelReservation.Guests.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FC4.HotelReservation.Shared.Infrastructure.Mappings;

public class GuestConfiguration: IEntityTypeConfiguration<Guest>
{
    public void Configure(EntityTypeBuilder<Guest> builder)
    {
        builder.ToTable("guests");
        
        builder.HasKey(g => g.Id);
        builder.Property(g => g.Id)
            .HasColumnName("id")
            .ValueGeneratedNever();
            
        builder.Property(g => g.FirstName)
            .HasColumnName("first_name")
            .HasMaxLength(100)
            .IsRequired();
            
        builder.Property(g => g.LastName)
            .HasColumnName("last_name")
            .HasMaxLength(100)
            .IsRequired();
            
        builder.OwnsOne(g => g.Email, email =>
        {
            email.Property(e => e.Value)
                .HasColumnName("email")
                .HasMaxLength(255)
                .IsRequired();
        });

        builder.Ignore(g => g.Events);
    }
}