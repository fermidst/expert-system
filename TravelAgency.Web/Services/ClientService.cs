using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TravelAgency.Infrastructure;
using TravelAgency.Web.Dtos;

namespace TravelAgency.Web.Services
{
    public class ClientService : IClientService
    {
        private readonly IApplicationDbContext _dbContext;

        public ClientService(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Infrastructure.Models.Client> GetClientAsync(long clientId)
        {
            return await _dbContext.Clients.SingleOrDefaultAsync(client => client.Id == clientId);
        }

        public IQueryable<Infrastructure.Models.Client> GetClientsAsync()
        {
            return _dbContext.Clients;
        }

        public async Task<Infrastructure.Models.Client> UpdateClientAsync(long clientId, ClientRequestDto client)
        {
            var entity = await _dbContext.Clients.SingleOrDefaultAsync(c => c.Id == clientId);
            entity.FullName = client.FullName;
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Infrastructure.Models.Client> CreateClientAsync(ClientRequestDto client)
        {
            var entry = await _dbContext.Clients.AddAsync(new Infrastructure.Models.Client
            {
                FullName = client.FullName,
                TicketId = client.TicketId
            });
            await _dbContext.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task DeleteClientAsync(long clientId)
        {
            var entity = await _dbContext.Clients.SingleOrDefaultAsync(c => c.Id == clientId);
            _dbContext.Clients.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}