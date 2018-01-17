using System;
using System.Collections.Generic;

namespace MovieCineplex.Models
{
    public partial class SessionTime
    {
        public SessionTime()
        {
            Reservation = new HashSet<Reservation>();
        }

        public int SessionId { get; set; }
        public int CineplexId { get; set; }
        public int MovieId { get; set; }
        public DateTime MovieTime { get; set; }

        public virtual ICollection<Reservation> Reservation { get; set; }
        public virtual Cineplex Cineplex { get; set; }
        public virtual Movie Movie { get; set; }
    }
}
