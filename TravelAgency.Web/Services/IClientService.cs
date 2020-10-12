using System.Threading.Tasks;
using TravelAgency.Web.Dtos;

namespace TravelAgency.Web.Services
{
    public interface IClientService
    {
        Task<Infrastructure.Models.Client> GetClient(long clientId);

        Task<Infrastructure.Models.Client> UpdateClient(long clientId, SaveClient client);

        Task<Infrastructure.Models.Client> CreateClient(SaveClient client);

        Task DeleteClient(long clientId);
    }
}