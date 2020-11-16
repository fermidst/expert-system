using System;
using System.Linq;
using System.Threading.Tasks;
using Jewelry.Infrastructure;
using Jewelry.Infrastructure.Models;
using Jewelry.Web.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Jewelry.Web.Services
{
    public class ClientService : IClientService
    {
        private readonly IApplicationDbContext _dbContext;

        public ClientService(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Client> GetClientAsync(long clientId)
        {
            return await _dbContext.Clients.SingleOrDefaultAsync(client => client.Id == clientId);
        }

        public IQueryable<Client> GetClientsAsync()
        {
            return _dbContext.Clients;
        }

        public async Task<Client> UpdateClientAsync(long clientId, ClientRequestDto client)
        {
            var entity = await _dbContext.Clients.SingleOrDefaultAsync(c => c.Id == clientId);
            entity.FullName = client.FullName;
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Client> CreateClientAsync(ClientRequestDto client)
        {
            var entry = await _dbContext.Clients.AddAsync(new Client
            {
                FullName = client.FullName,
                SoldDate = DateTime.UtcNow,
                JewelryId = client.JewelryId
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