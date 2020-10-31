using System;
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

        public async Task<Infrastructure.Models.Ticket> UpdateTicketAsync(long ticketId, TicketRequestDto ticketRequestDto)
        {
            if (ticketRequestDto.StartDate < DateTime.UtcNow || ticketRequestDto.EndDate < DateTime.UtcNow)
            {
                throw new ArgumentException("date is invalid");
            }
            var entity = await _dbContext.Tickets.SingleOrDefaultAsync(t => t.Id == ticketId);
            entity.Address = ticketRequestDto.Address;
            entity.HotelClass = ticketRequestDto.HotelClass;
            entity.StartDate = ticketRequestDto.StartDate;
            entity.EndDate = ticketRequestDto.EndDate;
            entity.StartTime = TimeSpan.Parse(ticketRequestDto.StartTime);
            entity.EndTime = TimeSpan.Parse(ticketRequestDto.EndTime);
            entity.IsAllInclusive = ticketRequestDto.IsAllInclusive;
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Infrastructure.Models.Ticket> CreateTicketAsync(TicketRequestDto ticketRequestDto)
        {
            if (ticketRequestDto.StartDate < DateTime.UtcNow || ticketRequestDto.EndDate < DateTime.UtcNow)
            {
                throw new ArgumentException("date is invalid");
            }
            var entry = await _dbContext.Tickets.AddAsync(new Infrastructure.Models.Ticket
            {
                Address = ticketRequestDto.Address,
                HotelClass = ticketRequestDto.HotelClass,
                StartDate = ticketRequestDto.StartDate,
                EndDate = ticketRequestDto.EndDate,
                StartTime = TimeSpan.Parse(ticketRequestDto.StartTime),
                EndTime = TimeSpan.Parse(ticketRequestDto.EndTime),
                IsAllInclusive = ticketRequestDto.IsAllInclusive
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