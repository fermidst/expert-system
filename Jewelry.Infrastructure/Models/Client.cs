using System;

namespace Jewelry.Infrastructure.Models
{
    public class Client
    {
        public long Id { get; set; }

        public string FullName { get; set; }

        public DateTime SoldDate { get; set; }

        public long JewelryId { get; set; }
        public virtual Jewelry Jewelry { get; set; }
    }
}