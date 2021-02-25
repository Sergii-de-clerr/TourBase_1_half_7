using System;
using System.Collections.Generic;

#nullable disable

namespace TourBase_Stage_1_2
{
    public partial class Voucher
    {
        public int VoucherId { get; set; }
        public DateTime? TakeOffDate { get; set; }
        public int? TouristId { get; set; }
        public int? TourId { get; set; }

        public virtual Tour Tour { get; set; }
        public virtual Tourist Tourist { get; set; }
    }
}
