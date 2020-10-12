using Microsoft.EntityFrameworkCore;
using TravelAgency.Infrastructure.Models;

namespace TravelAgency.Infrastructure
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Client>(builder =>
            {
                builder.ToTable("Clients");
                builder.HasKey(client => client.Id);
                builder.HasOne(client => client.Ticket)
                    .WithMany(ticket => ticket.Clients)
                    .HasForeignKey(client => client.TicketId);
            });
            modelBuilder.Entity<Ticket>(builder =>
            {
                builder.ToTable("Tickets");
                builder.HasKey(ticket => ticket.Id);
                builder.Property(ticket => ticket.HotelClass).HasConversion<int>();
            });
        }

        public DbSet<Client> Clients { get; set; }
        
        public DbSet<Ticket> Tickets { get; set; }
    }
}