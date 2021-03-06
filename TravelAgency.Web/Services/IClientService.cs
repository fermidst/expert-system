﻿using System.Linq;
using System.Threading.Tasks;
using TravelAgency.Web.Dtos;

namespace TravelAgency.Web.Services
{
    public interface IClientService
    {
        Task<Infrastructure.Models.Client> GetClientAsync(long clientId);

        Task<Infrastructure.Models.Client> UpdateClientAsync(long clientId, ClientRequestDto client);

        Task<Infrastructure.Models.Client> CreateClientAsync(ClientRequestDto client);

        IQueryable<Infrastructure.Models.Client> GetClientsAsync();

        Task DeleteClientAsync(long clientId);
    }
}