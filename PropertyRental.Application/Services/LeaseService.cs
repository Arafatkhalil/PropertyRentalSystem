using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PropertyRental.Application.DTOs;
using PropertyRental.Application.Interfaces;
using PropertyRental.Domain.Entities;

namespace PropertyRental.Application.Services
{
    public class LeaseService : ILeaseService
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public LeaseService(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> CreateLeaseAsync(CreateLeaseDto dto)
        {
            var property = await _context.Properties.FindAsync(dto.PropertyId);
            if (property == null)
                throw new KeyNotFoundException($"Property with Id {dto.PropertyId} was not found.");

            // Rule 1: A property cannot be leased if it is not available
            if (!property.IsAvailable)
                throw new InvalidOperationException("Cannot create a lease for a property that is not available.");

            // Rule 2: A property cannot have overlapping lease periods
            bool hasOverlap = await _context.Leases
                .AnyAsync(l => l.PropertyId == dto.PropertyId
                    && l.StartDate < dto.EndDate
                    && l.EndDate > dto.StartDate);

            if (hasOverlap)
                throw new InvalidOperationException("The property already has a lease that overlaps with the requested period.");

            var tenantExists = await _context.Tenants.AnyAsync(t => t.Id == dto.TenantId);
            if (!tenantExists)
                throw new KeyNotFoundException($"Tenant with Id {dto.TenantId} was not found.");

            var lease = _mapper.Map<Lease>(dto);
            _context.Leases.Add(lease);

            // Rule 3: When a lease is created, the property must automatically become unavailable
            property.IsAvailable = false;

            await _context.SaveChangesAsync(default);
            return lease.Id;
        }

        public async Task<IEnumerable<LeaseDto>> GetLeasesByPropertyIdAsync(int propertyId)
        {
            var leases = await _context.Leases
                .Where(l => l.PropertyId == propertyId)
                .OrderByDescending(l => l.StartDate)
                .ToListAsync();

            return _mapper.Map<IEnumerable<LeaseDto>>(leases);
        }

        public async Task EndLeaseAsync(int id)
        {
            var lease = await _context.Leases
                .Include(l => l.Property)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (lease == null)
                throw new KeyNotFoundException($"Lease with Id {id} was not found.");

            lease.EndDate = DateTime.UtcNow;
            lease.Property.IsAvailable = true;

            await _context.SaveChangesAsync(default);
        }
    }
}
