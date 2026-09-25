using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniBooking.Domain.Entities
{
    public class Resource : BaseEntity
    {
        public Guid TenantId { get; set; }
        public Tenant Tenant { get; set; } = null!;

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public string Name { get; set; } =string.Empty;
        public string? Description { get; set; }

        public int Capacity { get; set; }
        public string Location { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }
}
