using System;
using System.Collections.Generic;

#nullable disable

namespace TravelTogether2.Models
{
    public partial class TripActivity
    {
        public int Id { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? Price { get; set; }
        public int? TripId { get; set; }
        public int? TourActivityId { get; set; }

        public virtual TourActivity TourActivity { get; set; }
        public virtual Trip Trip { get; set; }
    }
}
