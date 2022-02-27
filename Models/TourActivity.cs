using System;
using System.Collections.Generic;

#nullable disable

namespace TravelTogether2.Models
{
    public partial class TourActivity
    {
        public TourActivity()
        {
            TripActivities = new HashSet<TripActivity>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? Price { get; set; }
        public int? PlaceId { get; set; }
        public bool? IsExtra { get; set; }
        public int? TourId { get; set; }
        public bool? MainActivityId { get; set; }

        public virtual Place Place { get; set; }
        public virtual Tour Tour { get; set; }
        public virtual ICollection<TripActivity> TripActivities { get; set; }
    }
}
