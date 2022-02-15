using System;
using System.Collections.Generic;

#nullable disable

namespace TravelTogether2.Models
{
    public partial class Language
    {
        public Language()
        {
            HasLanguages = new HashSet<HasLanguage>();
        }

        public int Id { get; set; }
        public string Language1 { get; set; }
        public string Level { get; set; }

        public virtual ICollection<HasLanguage> HasLanguages { get; set; }
    }
}
