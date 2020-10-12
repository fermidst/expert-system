using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TravelAgency.Infrastructure.Models;

namespace TravelAgency.Infrastructure
{
    public interface IApplicationDbContext
    {
        DbSet<Client> Clients { get; set; }
        
        DbSet<Ticket> Tickets { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}