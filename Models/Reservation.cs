using System;
using System.Collections.Generic;

namespace MovieCineplex.Models
{
    public partial class Reservation
    {
        public int ReservationId { get; set; }
        public int EnquiryId { get; set; }
        public int SessionId { get; set; }
        public int SeatId { get; set; }

        public virtual Enquiry Enquiry { get; set; }
        public virtual SessionTime Session { get; set; }
    }
}
