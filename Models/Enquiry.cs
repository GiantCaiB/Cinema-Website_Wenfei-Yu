using System;
using System.Collections.Generic;

namespace MovieCineplex.Models
{
    public partial class Enquiry
    {
        public Enquiry()
        {
            EnquiryEvents = new HashSet<EnquiryEvents>();
            Reservation = new HashSet<Reservation>();
        }

        public int EnquiryId { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }

        public virtual ICollection<EnquiryEvents> EnquiryEvents { get; set; }
        public virtual ICollection<Reservation> Reservation { get; set; }
    }
}
