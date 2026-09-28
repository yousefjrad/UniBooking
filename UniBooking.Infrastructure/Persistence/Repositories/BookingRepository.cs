using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UniBooking.Application.Common.Interfaces;
using UniBooking.Domain.Entities;
using UniBooking.Domain.Enums;

namespace UniBooking.Infrastructure.Persistence.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly AppDbContext _context;

        public BookingRepository(AppDbContext context) => _context = context;

        public  async Task<bool> HasConflictAsync(Guid resourceId, DateTime start, DateTime end)
        {
            return await _context.Bookings
                .AnyAsync(b => b.ResourceId == resourceId
                && b.Resource.IsActive
                && b.Status != BookingStatus.Cancelled
                && start < b.EndTime && end > b.StartTime);
        }
        public async Task AddAsync(Booking booking)
        {
            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Booking>> GetByUserAsync(Guid userId)
        {
            return await _context.Bookings
                .AsNoTracking()
                .Include(b => b.Resource)
                .Where(b => b.UserId == userId)
                .OrderByDescending(b => b.StartTime)
                .ToListAsync();
        }
    }
    
}
