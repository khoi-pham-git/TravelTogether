using System;
using System.Collections.Generic;

#nullable disable

namespace TravelTogether2.Models
{
    public partial class TravelAgency
    {
        public TravelAgency()
        {
            Areas = new HashSet<Area>();
            TourGuides = new HashSet<TourGuide>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }

        public virtual ICollection<Area> Areas { get; set; }
        public virtual ICollection<TourGuide> TourGuides { get; set; }
    }
}
