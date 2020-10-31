using System.Linq;
using System.Threading.Tasks;
using TravelAgency.Web.Dtos;

namespace TravelAgency.Web.Services
{
    public interface ITicketService
    {
        Task<Infrastructure.Models.Ticket> GetTicketAsync(long ticketId);

        Task<Infrastructure.Models.Ticket> UpdateTicketAsync(long ticketId, TicketRequestDto ticketRequestDto);

        Task<Infrastructure.Models.Ticket> CreateTicketAsync(TicketRequestDto ticketRequestDto);

        Task DeleteTicketAsync(long ticketId);
        
        IQueryable<Infrastructure.Models.Ticket> GetTickets();
    }
}