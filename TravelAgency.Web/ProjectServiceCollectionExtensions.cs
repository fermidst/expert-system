using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TravelAgency.Infrastructure;
using TravelAgency.Web.Services;

namespace TravelAgency.Web
{
    internal static class ProjectServiceCollectionExtensions
    {
        public static IServiceCollection AddProjectMappers(this IServiceCollection services) =>
            services.AddAutoMapper(typeof(Startup));
        public static IServiceCollection AddProjectServices(this IServiceCollection services,
            IConfiguration configuration) =>
            services
                .AddDbContext<ApplicationDbContext>(builder =>
                    builder.UseSqlite(configuration.GetConnectionString("ApplicationDbContext")))
                .AddScoped<IApplicationDbContext, ApplicationDbContext>()
                .AddScoped<IClientService, ClientService>()
                .AddScoped<ITicketService, TicketService>();
    }
}