using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniBooking.Domain.Entities;

namespace UniBooking.Application.Common.Interfaces
{
    public interface IBookingRepository
    {
        Task<bool> HasConflictAsync(Guid resourceId, DateTime start, DateTime end);
        Task AddAsync(Booking booking);
        Task<List<Booking>> GetByUserAsync(Guid userId);
    }
}
