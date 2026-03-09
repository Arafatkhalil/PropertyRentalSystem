using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PropertyRental.Application.Interfaces;
using PropertyRental.Application.Services;
using PropertyRental.Infrastructure.Persistence;

namespace PropertyRental.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

            services.AddScoped<IPropertyService, PropertyService>();
            services.AddScoped<ITenantService, TenantService>();
            services.AddScoped<ILeaseService, LeaseService>();

            return services;
        }
    }
}
