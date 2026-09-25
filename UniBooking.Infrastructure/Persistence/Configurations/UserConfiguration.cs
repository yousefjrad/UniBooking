using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using UniBooking.Domain.Entities;

namespace UniBooking.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.FirstName).IsRequired().HasMaxLength(100);
        builder.Property(u => u.LastName).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Email).IsRequired().HasMaxLength(200);
        builder.HasIndex(u => u.Email).IsUnique();

        builder.Property(u => u.Role)
               .HasConversion<string>()
               .HasMaxLength(50);

        builder.HasOne(u => u.Tenant)
               .WithMany(t => t.Users)
               .HasForeignKey(u => u.TenantId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}