using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniBooking.Domain.Entities
{
    public class Tenant : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public ICollection<Resource> Resources { get; set; } = new List<Resource>();
        public ICollection<User> Users { get; set; } = new List<User>();

    }
}
