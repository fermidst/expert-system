using System.Linq;
using System.Threading.Tasks;
using Jewelry.Infrastructure.Models;
using Jewelry.Web.Dtos;

namespace Jewelry.Web.Services
{
    public interface IJewelryService
    {
        Task<Infrastructure.Models.Jewelry> GetJewelryAsync(long jewelryId);

        Task<Infrastructure.Models.Jewelry> UpdateJewelryAsync(long jewelryId, JewelryRequestDto jewelryRequestDto);

        Task<Infrastructure.Models.Jewelry> CreateJewelryAsync(JewelryRequestDto jewelryRequestDto);

        Task DeleteJewelryAsync(long jewelryId);
        
        IQueryable<Infrastructure.Models.Jewelry> GetJewelries();
    }
}