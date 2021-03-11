using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace TourBase_Stage_1_2
{
    public partial class Tourist
    {
        public Tourist()
        {
            Vouchers = new HashSet<Voucher>();
        }

        public int TouristId { get; set; }

        [Required(ErrorMessage = "Це поле повинно бути заповнене")]
        [Display(Name = "ПIБ туриста")]
        public string TouristName { get; set; }

        [Required(ErrorMessage = "Це поле повинно бути заповнене")]
        [Display(Name = "Дата народження")]
        public DateTime? BirthDate { get; set; }

        [Required(ErrorMessage = "Це поле повинно бути заповнене")]
        [Display(Name = "Поштова адресса")]
        public string EmailAdress { get; set; }

        public virtual ICollection<Voucher> Vouchers { get; set; }
    }
}
