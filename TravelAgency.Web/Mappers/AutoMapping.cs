using AutoMapper;
using TravelAgency.Infrastructure.Models;

namespace TravelAgency.Web.Mappers
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Ticket, Dtos.Ticket>().ForSourceMember(ticket => ticket.Clients, opt => opt.DoNotValidate());
            CreateMap<Client, Dtos.Client>().ForSourceMember(client => client.Ticket, opt => opt.DoNotValidate());
        }
    }
}