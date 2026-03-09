using PropertyRental.Application.DTOs;

namespace PropertyRental.Application.Interfaces
{
    public interface IPropertyService
    {
        Task<PagedResult<PropertyDto>> GetAllPropertiesAsync(string? city = null, bool? isAvailable = null, int page = 1, int pageSize = 10);
        Task<PropertyDto?> GetPropertyByIdAsync(int id);
        Task<int> CreatePropertyAsync(CreatePropertyDto dto);
        Task UpdatePropertyAsync(int id, UpdatePropertyDto dto);
        Task DeletePropertyAsync(int id);
    }
}
