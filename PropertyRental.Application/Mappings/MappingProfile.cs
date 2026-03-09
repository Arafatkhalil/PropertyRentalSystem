using AutoMapper;
using PropertyRental.Application.DTOs;
using PropertyRental.Domain.Entities;

namespace PropertyRental.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Property, PropertyDto>();
            CreateMap<CreatePropertyDto, Property>();
            CreateMap<UpdatePropertyDto, Property>();

            CreateMap<Tenant, TenantDto>();
            CreateMap<CreateTenantDto, Tenant>();
            CreateMap<UpdateTenantDto, Tenant>();

            CreateMap<Lease, LeaseDto>();
            CreateMap<CreateLeaseDto, Lease>();
        }
    }
}
