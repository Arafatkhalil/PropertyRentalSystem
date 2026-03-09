using PropertyRental.Application.DTOs;

namespace PropertyRental.Application.Interfaces
{
    public interface IPropertyService
    {
        Task<IEnumerable<PropertyDto>> GetAllPropertiesAsync(string? city = null, bool? isAvailable = null);
        Task<PropertyDto?> GetPropertyByIdAsync(int id);
        Task<int> CreatePropertyAsync(CreatePropertyDto dto);
        Task UpdatePropertyAsync(int id, UpdatePropertyDto dto);
        Task DeletePropertyAsync(int id);
    }
}
