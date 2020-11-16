using System;

namespace Jewelry.Web.Dtos
{
    public class ClientResponseDto
    {
        public long Id { get; set; }

        public string FullName { get; set; }

        public DateTime SoldDate { get; set; }

        public long JewelryId { get; set; }
    }
}