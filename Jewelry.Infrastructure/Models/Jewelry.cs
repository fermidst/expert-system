using System;
using System.Collections.Generic;

namespace Jewelry.Infrastructure.Models
{
    public class Jewelry
    {
        public long Id { get; set; }

        public string Name { get; set; }
        
        public decimal Price { get; set; }

        public DateTime ReceivedDate { get; set; }
        
        public string PhotoUrl { get; set; }

        public virtual ICollection<Client> Clients { get; set; }
    }
}