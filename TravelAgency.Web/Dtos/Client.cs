using System;

namespace TravelAgency.Web.Dtos
{
    public class Client
    {
        public long Id { get; set; }

        public string FullName { get; set; }

        public DateTime DepartureDate { get; set; }
        
        public long TicketId { get; set; }
    }
}