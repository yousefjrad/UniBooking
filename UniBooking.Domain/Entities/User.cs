using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniBooking.Domain.Enums;

namespace UniBooking.Domain.Entities
{
    public class User : BaseEntity
    {
        public Guid TenantId { get; set; }

        public Tenant Tenant { get; set; } = null!;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; }= string.Empty;
        public string Email { get; set; }=string.Empty;
        public string PasswordHash { get; set; }= string.Empty ;

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public UserRole Role { get; set; }
    }
}
