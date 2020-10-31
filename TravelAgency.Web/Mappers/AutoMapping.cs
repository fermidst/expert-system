using AutoMapper;
using TravelAgency.Infrastructure.Models;

namespace TravelAgency.Web.Mappers
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Ticket, Dtos.TicketResponseDto>().ForSourceMember(ticket => ticket.Clients, opt => opt.DoNotValidate());
            CreateMap<Client, Dtos.ClientResponseDto>().ForSourceMember(client => client.Ticket, opt => opt.DoNotValidate());
        }
    }
}