using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace TourBase_Stage_1_2
{
    public partial class Stage
    {
        public int StageId { get; set; }

        [Required(ErrorMessage = "Це поле повинно бути заповнене")]
        [Display(Name = "Номер етапу")]
        public int? StageNumber { get; set; }

        [Required(ErrorMessage = "Це поле повинно бути заповнене")]
        [Display(Name = "Номер туру")]
        public int? TourId { get; set; }

        [Required(ErrorMessage = "Це поле повинно бути заповнене")]
        [Display(Name = "Номер транспорту")]
        public int? TransportId { get; set; }

        [Required(ErrorMessage = "Це поле повинно бути заповнене")]
        [Display(Name = "Номер готелю")]
        public int? HotelId { get; set; }

        public virtual Hotel Hotel { get; set; }
        public virtual Tour Tour { get; set; }
        public virtual Transport Transport { get; set; }
    }
}
