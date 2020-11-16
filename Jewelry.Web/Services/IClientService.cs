using System.Linq;
using System.Threading.Tasks;
using Jewelry.Infrastructure.Models;
using Jewelry.Web.Dtos;

namespace Jewelry.Web.Services
{
    public interface IClientService
    {
        Task<Client> GetClientAsync(long clientId);

        Task<Client> UpdateClientAsync(long clientId, ClientRequestDto client);

        Task<Client> CreateClientAsync(ClientRequestDto client);

        IQueryable<Client> GetClientsAsync();

        Task DeleteClientAsync(long clientId);
    }
}