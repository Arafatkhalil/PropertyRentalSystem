using Microsoft.EntityFrameworkCore;
using PropertyRental.Application.Interfaces;
using PropertyRental.Domain.Entities;

namespace PropertyRental.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Property> Properties => Set<Property>();
        public DbSet<Tenant> Tenants => Set<Tenant>();
        public DbSet<Lease> Leases => Set<Lease>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Property>().Property(p => p.MonthlyPrice).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Lease>().Property(l => l.MonthlyPrice).HasColumnType("decimal(18,2)");

            // Global Query Filters for Soft Delete
            modelBuilder.Entity<Property>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<Tenant>().HasQueryFilter(t => !t.IsDeleted);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}