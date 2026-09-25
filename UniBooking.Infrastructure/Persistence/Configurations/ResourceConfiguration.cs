using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using UniBooking.Domain.Entities;

namespace UniBooking.Infrastructure.Persistence.Configurations;

public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
{
    public void Configure(EntityTypeBuilder<Resource> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Name).IsRequired().HasMaxLength(150);
        builder.Property(r => r.Location).HasMaxLength(200);

        builder.HasOne(r => r.Tenant)
               .WithMany(t => t.Resources)
               .HasForeignKey(r => r.TenantId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}