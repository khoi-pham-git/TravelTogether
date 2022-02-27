using System;
using System.Collections.Generic;

#nullable disable

namespace TravelTogether2.Models
{
    public partial class Tour
    {
        public Tour()
        {
            TourActivities = new HashSet<TourActivity>();
            Trips = new HashSet<Trip>();
        }

        public int Id { get; set; }
        public int? QuatityTrip { get; set; }
        public string Name { get; set; }
        public int? Price { get; set; }
        public bool? Status { get; set; }
        public int? TourGuideId { get; set; }

        public virtual TourGuide TourGuide { get; set; }
        public virtual ICollection<TourActivity> TourActivities { get; set; }
        public virtual ICollection<Trip> Trips { get; set; }
    }
}
