using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [Required(ErrorMessage = "Це поле повинно бути заповнене")]
        [Display(Name = "Назва готелю")]
        public string HotelName { get; set; }
        public string CategoryOfTheService { get; set; }
        public int? CityId { get; set; }

        public virtual City City { get; set; }
        public virtual ICollection<Stage> Stages { get; set; }
    }
}
