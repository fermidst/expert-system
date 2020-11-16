using Jewelry.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Jewelry.Infrastructure
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
                builder.HasOne(client => client.Jewelry)
                    .WithMany(jewelry => jewelry.Clients)
                    .HasForeignKey(client => client.JewelryId);
            });
            modelBuilder.Entity<Models.Jewelry>(builder =>
            {
                builder.ToTable("Jewelries");
                builder.HasKey(jewelry => jewelry.Id);
            });
        }

        public DbSet<Client> Clients { get; set; }
        
        public DbSet<Models.Jewelry> Jewelries { get; set; }
    }
}