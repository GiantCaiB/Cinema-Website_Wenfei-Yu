using System;
using System.Collections.Generic;

namespace MovieCineplex.Models
{
    public partial class EnquiryEvents
    {
        public int EventsId { get; set; }
        public int EnquiryId { get; set; }

        public virtual Enquiry Enquiry { get; set; }
        public virtual EventsInfo Events { get; set; }
    }
}
