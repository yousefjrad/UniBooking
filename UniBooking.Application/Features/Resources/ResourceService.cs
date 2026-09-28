using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniBooking.Application.Common.Interfaces;
using UniBooking.Application.Features.Resources.Dtos;
using UniBooking.Domain.Entities;

namespace UniBooking.Application.Features.Resources
{
    public class ResourceService
    {
        private readonly IResourceRepository _resourceRepository;

        public ResourceService(IResourceRepository resourceRepository)
        {
            _resourceRepository = resourceRepository;
        }

        public async Task<ResourceDto> CreateAsync (CreateResourceDto dto , Guid TenantId)
        {
            var resource = new Resource
            {
                TenantId = TenantId,
                Name = dto.Name,
                Description = dto.Description,
                Capacity = dto.Capacity,
                Location = dto.Location,
            };

            await _resourceRepository.AddAsync(resource);
            return MapToDto(resource);
        }
        public async Task<List<ResourceDto>> GetAllAsync(Guid tenantId)
        {
            var resources = await _resourceRepository.GetAllAsync(tenantId);
            return  resources.Select(r => MapToDto(r)).ToList();
                
        }
   
        public async Task<ResourceDto?> GetByIdAsync(Guid id)
        {
            var resource = await _resourceRepository.GetByIdAsync(id);
            return resource is null ? null : MapToDto(resource) ;
        }

        public async Task<List<ResourceDto>> GetAvailableAsync(Guid tenantId, int minCapacity)
        {
            var resources = await _resourceRepository.GetAvailableAsync(tenantId, minCapacity);
            return resources.Select(MapToDto).ToList();
        }

        private static ResourceDto MapToDto(Resource r)
        => new (r.Id, r.Name, r.Description, r.Capacity, r.Location, r.IsActive);
    }
}
