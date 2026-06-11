using ContactsListSite.Application.Services;
using ContactsListSite.Application.ServInterfaces;
using ContactsListSite.Domain.RepInterfaces;
using ContactsListSite.Infrastructure.Data;
using ContactsListSite.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;

namespace ContactsListSite.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IContactService, ContactService>();

            services.AddScoped<IContactRepository, ContactRepository>();

            return services;
        }
    }
}
