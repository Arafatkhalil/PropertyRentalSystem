using Microsoft.EntityFrameworkCore;
using PropertyRental.Domain.Entities;
using System.Collections.Generic;

namespace PropertyRental.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Property> Properties { get; }
        DbSet<Tenant> Tenants { get; }
        DbSet<Lease> Leases { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}