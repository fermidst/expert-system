using System.Threading;
using System.Threading.Tasks;
using Jewelry.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Jewelry.Infrastructure
{
    public interface IApplicationDbContext
    {
        DbSet<Client> Clients { get; set; }
        
        DbSet<Models.Jewelry> Jewelries { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}