using System;
using System.Collections.Generic;

#nullable disable

namespace TourBase_Stage_1_2
{
    public partial class Transport
    {
        public Transport()
        {
            Stages = new HashSet<Stage>();
        }

        public int TransportId { get; set; }
        public string TransportName { get; set; }

        public virtual ICollection<Stage> Stages { get; set; }
    }
}
