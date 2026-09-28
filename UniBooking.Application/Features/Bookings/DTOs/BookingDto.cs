using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniBooking.Domain.Enums;

namespace UniBooking.Application.Features.Bookings.DTOs
{
    public record BookingDto(
        Guid Id,
        Guid ResourceId , 
        string UserFullName,
        string ResourceName,
        DateTime StartTime , 
        DateTime EndTime ,
        string Status,
        string? notes
        );
    
}
