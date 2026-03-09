using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PropertyRental.Application.DTOs;
using PropertyRental.Application.Interfaces;
using PropertyRental.Domain.Entities;

namespace PropertyRental.Application.Services
{
    public class TenantService : ITenantService
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TenantService(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TenantDto>> GetAllTenantsAsync()
        {
            var tenants = await _context.Tenants.ToListAsync();
            return _mapper.Map<IEnumerable<TenantDto>>(tenants);
        }

        public async Task<TenantDto?> GetTenantByIdAsync(int id)
        {
            var tenant = await _context.Tenants.FindAsync(id);
            if (tenant == null)
                return null;

            return _mapper.Map<TenantDto>(tenant);
        }

        public async Task<int> CreateTenantAsync(CreateTenantDto dto)
        {
            var tenant = _mapper.Map<Tenant>(dto);
            _context.Tenants.Add(tenant);
            await _context.SaveChangesAsync(default);
            return tenant.Id;
        }

        public async Task UpdateTenantAsync(int id, UpdateTenantDto dto)
        {
            var tenant = await _context.Tenants.FindAsync(id);
            if (tenant == null)
                throw new KeyNotFoundException($"Tenant with Id {id} was not found.");

            tenant.FullName = dto.FullName;
            tenant.Phone = dto.Phone;
            tenant.Email = dto.Email;
            tenant.NationalId = dto.NationalId;

            await _context.SaveChangesAsync(default);
        }

        public async Task DeleteTenantAsync(int id)
        {
            var tenant = await _context.Tenants.FindAsync(id);
            if (tenant == null)
                throw new KeyNotFoundException($"Tenant with Id {id} was not found.");

            _context.Tenants.Remove(tenant);
            await _context.SaveChangesAsync(default);
        }
    }
}
