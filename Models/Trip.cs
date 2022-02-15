using System;
using System.Collections.Generic;

#nullable disable

namespace TravelTogether2.Models
{
    public partial class Trip
    {
        public Trip()
        {
            Payments = new HashSet<Payment>();
            TripActivities = new HashSet<TripActivity>();
        }

        public int Id { get; set; }
        public DateTime? BookingDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? FeedBackCore { get; set; }
        public bool? Status { get; set; }
        public string Feedback { get; set; }
        public int? Price { get; set; }
        public int? TourId { get; set; }
        public int? CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Tour Tour { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<TripActivity> TripActivities { get; set; }
    }
}
