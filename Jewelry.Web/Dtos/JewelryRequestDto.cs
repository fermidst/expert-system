using System;
using Jewelry.Infrastructure.Models;

namespace Jewelry.Web.Dtos
{
    public class JewelryRequestDto
    {
        public string Name { get; set; }
        
        public decimal Price { get; set; }

        public DateTime ReceivedDate { get; set; }

        public string PhotoUrl { get; set; }
    }
}