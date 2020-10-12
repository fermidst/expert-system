using System;

namespace TravelAgency.Web.Dtos
{
    public class SaveTicket
    {
        public string Address { get; set; }

        // todo: can be refactored?
        public Infrastructure.Models.Ticket.HotelClassType HotelClass { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public bool IsAllInclusive { get; set; }
    }
}