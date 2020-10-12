using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TravelAgency.Infrastructure;
using TravelAgency.Infrastructure.Models;
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

        public async Task<Infrastructure.Models.Client> GetClient(long clientId)
        {
            return await _dbContext.Clients.SingleOrDefaultAsync(client => client.Id == clientId);
        }

        public async Task<Infrastructure.Models.Client> UpdateClient(long clientId, SaveClient client)
        {
            var entity = await _dbContext.Clients.SingleOrDefaultAsync(c => c.Id == clientId);
            entity.DepartureDate = client.DepartureDate;
            entity.FullName = client.FullName;
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Infrastructure.Models.Client> CreateClient(SaveClient client)
        {
            var entry = await _dbContext.Clients.AddAsync(new Infrastructure.Models.Client
            {
                DepartureDate = client.DepartureDate,
                FullName = client.FullName
            });
            await _dbContext.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task DeleteClient(long clientId)
        {
            var entity = await _dbContext.Clients.SingleOrDefaultAsync(c => c.Id == clientId);
            _dbContext.Clients.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}