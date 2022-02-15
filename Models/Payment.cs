using System;
using System.Collections.Generic;

#nullable disable

namespace TravelTogether2.Models
{
    public partial class Payment
    {
        public int Id { get; set; }
        public int? Amount { get; set; }
        public string Type { get; set; }
        public DateTime? Date { get; set; }
        public int? TripId { get; set; }
        public string PaymentCode { get; set; }

        public virtual Trip Trip { get; set; }
    }
}
