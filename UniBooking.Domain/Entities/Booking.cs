using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniBooking.Domain.Enums;

namespace UniBooking.Domain.Entities
{
    public class Booking : BaseEntity
    {
        public Guid ResourceId { get; set; }
        public Resource Resource { get; set; } = null!;

        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public DateTime StartTime { get; set; } 

        public DateTime EndTime { get; set; } 

        public BookingStatus Status { get; set; } = BookingStatus.Pending;
        public string? Notes { get; set; }
    }
}
