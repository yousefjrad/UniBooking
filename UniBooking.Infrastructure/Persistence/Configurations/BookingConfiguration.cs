using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using UniBooking.Domain.Entities;

namespace UniBooking.Infrastructure.Persistence.Configurations;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Status)
               .HasConversion<string>()
               .HasMaxLength(50);

        builder.HasOne(b => b.Resource)
               .WithMany(r => r.Bookings)
               .HasForeignKey(b => b.ResourceId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(b => b.User)
               .WithMany(u => u.Bookings)
               .HasForeignKey(b => b.UserId)
               .OnDelete(DeleteBehavior.Restrict);

        // فهرس مركب لتسريع البحث عن تعارض الحجوزات
        builder.HasIndex(b => new { b.ResourceId, b.StartTime, b.EndTime });
    }
}