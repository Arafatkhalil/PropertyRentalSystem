using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PropertyRental.Application.DTOs;
using PropertyRental.Application.Interfaces;
using PropertyRental.Domain.Entities;

namespace PropertyRental.Application.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PropertyService(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagedResult<PropertyDto>> GetAllPropertiesAsync(string? city = null, bool? isAvailable = null, int page = 1, int pageSize = 10)
        {
            var query = _context.Properties.AsQueryable();

            if (!string.IsNullOrWhiteSpace(city))
                query = query.Where(p => p.City.Contains(city));

            if (isAvailable.HasValue)
                query = query.Where(p => p.IsAvailable == isAvailable.Value);

            var totalCount = await query.CountAsync();

            var properties = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<PropertyDto>
            {
                Items = _mapper.Map<List<PropertyDto>>(properties),
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            };
        }

        public async Task<PropertyDto?> GetPropertyByIdAsync(int id)
        {
            var property = await _context.Properties.FindAsync(id);
            if (property == null)
                return null;

            return _mapper.Map<PropertyDto>(property);
        }

        public async Task<int> CreatePropertyAsync(CreatePropertyDto dto)
        {
            var property = _mapper.Map<Property>(dto);
            _context.Properties.Add(property);
            await _context.SaveChangesAsync(default);
            return property.Id;
        }

        public async Task UpdatePropertyAsync(int id, UpdatePropertyDto dto)
        {
            var property = await _context.Properties.FindAsync(id);
            if (property == null)
                throw new KeyNotFoundException($"Property with Id {id} was not found.");

            property.Name = dto.Name;
            property.Address = dto.Address;
            property.City = dto.City;
            property.MonthlyPrice = dto.MonthlyPrice;
            property.IsAvailable = dto.IsAvailable;

            await _context.SaveChangesAsync(default);
        }

        // Soft Delete: instead of removing, mark as deleted
        public async Task DeletePropertyAsync(int id)
        {
            var property = await _context.Properties.FindAsync(id);
            if (property == null)
                throw new KeyNotFoundException($"Property with Id {id} was not found.");

            property.IsDeleted = true;
            await _context.SaveChangesAsync(default);
        }
    }
}
