using System;

namespace TravelAgency.Web.Dtos
{
    public class SaveClient
    {
        public string FullName { get; set; }

        public DateTime DepartureDate { get; set; }

        public long TicketId { get; set; }
    }
}