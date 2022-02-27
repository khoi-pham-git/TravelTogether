using System;
using System.Collections.Generic;

#nullable disable

namespace TravelTogether2.Models
{
    public partial class Place
    {
        public Place()
        {
            TourActivities = new HashSet<TourActivity>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string Longtitude { get; set; }
        public string Latitude { get; set; }
        public int? AreaId { get; set; }
        public int? CategoryId { get; set; }

        public virtual Area Area { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<TourActivity> TourActivities { get; set; }
    }
}
