using System;
using System.Collections.Generic;

#nullable disable

namespace TourBase_Stage_1_2
{
    public partial class Stage
    {
        public int StageId { get; set; }
        public int? StageNumber { get; set; }
        public int? TourId { get; set; }
        public int? TransportId { get; set; }
        public int? HotelId { get; set; }

        public virtual Hotel Hotel { get; set; }
        public virtual Tour Tour { get; set; }
        public virtual Transport Transport { get; set; }
    }
}
