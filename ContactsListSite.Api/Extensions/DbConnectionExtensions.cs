using ContactsListSite.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ContactsListSite.Api.Extensions
{
    public static class DbConnectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
