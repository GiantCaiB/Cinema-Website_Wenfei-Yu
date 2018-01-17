using System;
using System.Collections.Generic;

namespace MovieCineplex.Models
{
    public partial class EventsInfo
    {
        public EventsInfo()
        {
            EnquiryEvents = new HashSet<EnquiryEvents>();
        }

        public int EventsId { get; set; }
        public string Information { get; set; }

        public virtual ICollection<EnquiryEvents> EnquiryEvents { get; set; }
    }
}
