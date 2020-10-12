using System.Threading.Tasks;
using TravelAgency.Web.Dtos;

namespace TravelAgency.Web.Services
{
    public interface ITicketService
    {
        Task<Infrastructure.Models.Ticket> GetTicket(long ticketId);

        Task<Infrastructure.Models.Ticket> UpdateTicket(long ticketId, SaveTicket ticket);

        Task<Infrastructure.Models.Ticket> CreateTicket(SaveTicket ticket);

        Task DeleteTicket(long ticketId);
    }
}