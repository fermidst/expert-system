using AutoMapper;
using Jewelry.Infrastructure.Models;

namespace Jewelry.Web.Mappers
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Infrastructure.Models.Jewelry, Dtos.JewelryResponseDto>().ForSourceMember(jewelry => jewelry.Clients, opt => opt.DoNotValidate());
            CreateMap<Client, Dtos.ClientResponseDto>().ForSourceMember(client => client.Jewelry, opt => opt.DoNotValidate());
        }
    }
}