using PropertyRental.Application.DTOs;

namespace PropertyRental.Application.Interfaces
{
    public interface ITenantService
    {
        Task<IEnumerable<TenantDto>> GetAllTenantsAsync();
        Task<TenantDto?> GetTenantByIdAsync(int id);
        Task<int> CreateTenantAsync(CreateTenantDto dto);
        Task UpdateTenantAsync(int id, UpdateTenantDto dto);
        Task DeleteTenantAsync(int id);
    }
}
