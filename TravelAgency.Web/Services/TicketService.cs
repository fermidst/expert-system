using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TravelAgency.Infrastructure;
using TravelAgency.Web.Dtos;

namespace TravelAgency.Web.Services
{
    public class TicketService : ITicketService
    {
        private readonly IApplicationDbContext _dbContext;

        public TicketService(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Infrastructure.Models.Ticket> GetTicketAsync(long ticketId)
        {
            return await _dbContext.Tickets.SingleOrDefaultAsync(ticket => ticket.Id == ticketId);
        }

        public async Task<Infrastructure.Models.Ticket> UpdateTicketAsync(long ticketId, SaveTicket ticket)
        {
            var entity = await _dbContext.Tickets.SingleOrDefaultAsync(t => t.Id == ticketId);
            entity.Address = ticket.Address;
            entity.HotelClass = ticket.HotelClass;
            entity.StartTime = ticket.StartTime;
            entity.EndTime = ticket.EndTime;
            entity.IsAllInclusive = ticket.IsAllInclusive;
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Infrastructure.Models.Ticket> CreateTicketAsync(SaveTicket ticket)
        {
            var entry = await _dbContext.Tickets.AddAsync(new Infrastructure.Models.Ticket
            {
                Address = ticket.Address,
                HotelClass = ticket.HotelClass,
                StartTime = ticket.StartTime,
                EndTime = ticket.EndTime,
                IsAllInclusive = ticket.IsAllInclusive
            });
            await _dbContext.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task DeleteTicketAsync(long ticketId)
        {
            var entity = await _dbContext.Tickets.SingleOrDefaultAsync(t => t.Id == ticketId);
            _dbContext.Tickets.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
        
        public IQueryable<Infrastructure.Models.Ticket> GetTickets()
        {
            return _dbContext.Tickets;
        }
    }
}