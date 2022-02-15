using System;
using System.Collections.Generic;

#nullable disable

namespace TravelTogether2.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Follows = new HashSet<Follow>();
            Trips = new HashSet<Trip>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Image { get; set; }

        public virtual ICollection<Follow> Follows { get; set; }
        public virtual ICollection<Trip> Trips { get; set; }
    }
}
