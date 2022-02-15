using System;
using System.Collections.Generic;

#nullable disable

namespace TravelTogether2.Models
{
    public partial class HasLanguage
    {
        public int Id { get; set; }
        public int? TourGuideId { get; set; }
        public int? LanguageId { get; set; }

        public virtual Language Language { get; set; }
        public virtual TourGuide TourGuide { get; set; }
    }
}
