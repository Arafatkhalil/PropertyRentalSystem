using PropertyRental.Application.DTOs;

namespace PropertyRental.Application.Interfaces
{
    public interface ILeaseService
    {
        Task<int> CreateLeaseAsync(CreateLeaseDto dto);
        Task<IEnumerable<LeaseDto>> GetLeasesByPropertyIdAsync(int propertyId);
        Task EndLeaseAsync(int id);
    }
}
