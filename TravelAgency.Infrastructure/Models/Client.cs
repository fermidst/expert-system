using System;

namespace TravelAgency.Infrastructure.Models
{
    public class Client
    {
        public long Id { get; set; }

        public string FullName { get; set; }

        public long TicketId { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}