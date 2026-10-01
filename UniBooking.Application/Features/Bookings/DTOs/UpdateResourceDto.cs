using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniBooking.Domain.Entities;

namespace UniBooking.Application.Features.Bookings.DTOs
{
    public record class UpdateResourceDto (string Name ,string? Description , int Capacity , string Location ,bool IsActive );
    
    
}
