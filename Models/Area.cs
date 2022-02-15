using System;
using System.Collections.Generic;

#nullable disable

namespace TravelTogether2.Models
{
    public partial class Area
    {
        public Area()
        {
            Places = new HashSet<Place>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Latitude { get; set; }
        public string Longtitude { get; set; }
        public string TravelAgencyId { get; set; }

        public virtual TravelAgency TravelAgency { get; set; }
        public virtual ICollection<Place> Places { get; set; }
    }
}
