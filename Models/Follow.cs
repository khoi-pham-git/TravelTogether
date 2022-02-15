using System;
using System.Collections.Generic;

#nullable disable

namespace TravelTogether2.Models
{
    public partial class Follow
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public int? TourGuideId { get; set; }
        public bool? Status { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual TourGuide TourGuide { get; set; }
    }
}
