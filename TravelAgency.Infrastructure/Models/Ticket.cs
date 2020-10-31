using System;
using System.Collections.Generic;

namespace TravelAgency.Infrastructure.Models
{
    public class Ticket
    {
        public long Id { get; set; }

        public string Address { get; set; }

        public HotelClassType HotelClass { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public TimeSpan StartTime { get; set; }
        
        public TimeSpan EndTime { get; set; }

        public bool IsAllInclusive { get; set; }

        public virtual ICollection<Client> Clients { get; set; }
        
        public enum HotelClassType
        {
            OneStar = 1,
            TwoStar = 2,
            ThreeStar = 3,
            FourStar = 4,
            FiveStar = 5
        }
    }
}