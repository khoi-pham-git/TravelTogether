using System;
using System.Collections.Generic;

#nullable disable

namespace TravelTogether2.Models
{
    public partial class Category
    {
        public Category()
        {
            Places = new HashSet<Place>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Place> Places { get; set; }
    }
}
