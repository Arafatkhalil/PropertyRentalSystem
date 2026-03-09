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

        public async Task<IEnumerable<PropertyDto>> GetAllPropertiesAsync(string? city = null, bool? isAvailable = null)
        {
            var query = _context.Properties.AsQueryable();

            if (!string.IsNullOrWhiteSpace(city))
                query = query.Where(p => p.City.Contains(city));

            if (isAvailable.HasValue)
                query = query.Where(p => p.IsAvailable == isAvailable.Value);

            var properties = await query.ToListAsync();
            return _mapper.Map<IEnumerable<PropertyDto>>(properties);
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

        public async Task DeletePropertyAsync(int id)
        {
            var property = await _context.Properties.FindAsync(id);
            if (property == null)
                throw new KeyNotFoundException($"Property with Id {id} was not found.");

            _context.Properties.Remove(property);
            await _context.SaveChangesAsync(default);
        }
    }
}
