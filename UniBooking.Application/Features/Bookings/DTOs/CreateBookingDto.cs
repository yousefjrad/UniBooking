using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniBooking.Application.Features.Bookings.DTOs
{
    public record CreateBookingDto(Guid ResourceId,
    DateTime StartTime,
    DateTime EndTime,
    string? Notes);
    
    
}
