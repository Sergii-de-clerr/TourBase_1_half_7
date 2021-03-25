using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace TourBase_Stage_1_2
{
    public partial class Voucher
    {
        public int VoucherId { get; set; }

        [Required(ErrorMessage = "Це поле повинно бути заповнене")]
        [Display(Name = "Дата закінчення дії ваучера")]
        public DateTime? TakeOffDate { get; set; }

        [Required(ErrorMessage = "Це поле повинно бути заповнене")]
        [Display(Name = "ПIБ туриста")]
        public int? TouristId { get; set; }

        [Required(ErrorMessage = "Це поле повинно бути заповнене")]
        [Display(Name = "Назва туру")]
        public int? TourId { get; set; }

        public virtual Tour Tour { get; set; }
        public virtual Tourist Tourist { get; set; }
    }
}
