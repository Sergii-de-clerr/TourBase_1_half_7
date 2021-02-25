using System;
using System.Collections.Generic;

#nullable disable

namespace TourBase_Stage_1_2
{
    public partial class Hotel
    {
        public Hotel()
        {
            Stages = new HashSet<Stage>();
        }

        public int HotelId { get; set; }
        public string HotelName { get; set; }
        public string CategoryOfTheService { get; set; }
        public int? CityId { get; set; }

        public virtual City City { get; set; }
        public virtual ICollection<Stage> Stages { get; set; }
    }
}
