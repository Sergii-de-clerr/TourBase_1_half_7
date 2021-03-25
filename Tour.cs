using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace TourBase_Stage_1_2
{
    public partial class Tour
    {
        public Tour()
        {
            Stages = new HashSet<Stage>();
            Vouchers = new HashSet<Voucher>();
        }

        public int TourId { get; set; }

        [Required(ErrorMessage = "Це поле повинно бути заповнене")]
        [Display(Name = "Назва туру")]
        public string TourName { get; set; }

        [Required(ErrorMessage = "Це поле повинно бути заповнене")]
        [Display(Name = "Тривалість в днях")]
        public int? DurationInDays { get; set; }

        [Required(ErrorMessage = "Це поле повинно бути заповнене")]
        [Display(Name = "Інформація")]
        public string Info { get; set; }

        public virtual ICollection<Stage> Stages { get; set; }
        public virtual ICollection<Voucher> Vouchers { get; set; }
    }
}
