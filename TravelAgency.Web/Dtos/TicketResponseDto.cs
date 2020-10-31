using System;

namespace TravelAgency.Web.Dtos
{
    public class TicketResponseDto
    {
        public long Id { get; set; }

        public string Address { get; set; }

        // todo: can be refactored?
        public Infrastructure.Models.Ticket.HotelClassType HotelClass { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public bool IsAllInclusive { get; set; }
    }
}