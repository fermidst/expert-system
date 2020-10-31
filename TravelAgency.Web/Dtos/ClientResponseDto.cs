using System;

namespace TravelAgency.Web.Dtos
{
    public class ClientResponseDto
    {
        public long Id { get; set; }

        public string FullName { get; set; }

        public long TicketId { get; set; }
    }
}