using System;
using System.Collections.Generic;

#nullable disable

namespace TravelTogether2.Models
{
    public partial class TourGuide
    {
        public TourGuide()
        {
            Follows = new HashSet<Follow>();
            HasLanguages = new HashSet<HasLanguage>();
            Tours = new HashSet<Tour>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? Dob { get; set; }
        public bool? Gender { get; set; }
        public string Phone { get; set; } 
        public string Email { get; set; }
        public string SocialNumber { get; set; }
        public string Certification { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public int? Rank { get; set; }
        public int? AreaId { get; set; }
        public string TravelAgencyId { get; set; }
        public string Image { get; set; }

        public virtual TravelAgency TravelAgency { get; set; }
        public virtual ICollection<Follow> Follows { get; set; }
        public virtual ICollection<HasLanguage> HasLanguages { get; set; }
        public virtual ICollection<Tour> Tours { get; set; }
    }
}
