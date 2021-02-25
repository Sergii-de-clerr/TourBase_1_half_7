using System;
using System.Collections.Generic;

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
        public string TourName { get; set; }
        public int? DurationInDays { get; set; }
        public string Info { get; set; }

        public virtual ICollection<Stage> Stages { get; set; }
        public virtual ICollection<Voucher> Vouchers { get; set; }
    }
}
