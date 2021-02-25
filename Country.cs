using System;
using System.Collections.Generic;

#nullable disable

namespace TourBase_Stage_1_2
{
    public partial class Country
    {
        public Country()
        {
            Cities = new HashSet<City>();
        }

        public int CountryId { get; set; }
        public string CountryName { get; set; }

        public virtual ICollection<City> Cities { get; set; }
    }
}
