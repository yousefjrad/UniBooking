using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniBooking.Application.Common.Interfaces;
using UniBooking.Application.Features.Bookings.DTOs;
using UniBooking.Application.Features.Resources.Dtos;
using UniBooking.Domain.Entities;
using UniBooking.Domain.Exceptions;

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

        public async Task<ResourceDto?> GetByIdAsync(Guid id, Guid tenantId)
        {
            var resource = await _resourceRepository.GetByIdAsync(id, tenantId);
            return resource is null ? null : MapToDto(resource);
        }

        public async Task<List<ResourceDto>> GetAvailableAsync(Guid tenantId, int minCapacity)
        {
            var resources = await _resourceRepository.GetAvailableAsync(tenantId, minCapacity);
            return resources.Select(MapToDto).ToList();
        }

        private static ResourceDto MapToDto(Resource r)
        => new (r.Id, r.Name, r.Description, r.Capacity, r.Location, r.IsActive);

        public async Task UpdateAsync(Guid id, UpdateResourceDto dto, Guid tenantId)
        {
            var resource = await _resourceRepository.GetByIdAsync(id, tenantId)
                ?? throw new NotFoundException("Resource Not Exist");

            resource.Name = dto.Name;
            resource.Description = dto.Description;
            resource.Capacity = dto.Capacity;
            resource.Location = dto.Location;
            resource.IsActive = dto.IsActive;

            await _resourceRepository.UpdateAsync(resource);
        }

        public async Task DeleteAsync(Guid id, Guid tenantId)
        {
            var resource = await _resourceRepository.GetByIdAsync(id, tenantId)
                ?? throw new NotFoundException("Resource Not Exist");

            resource.IsActive = false;
            await _resourceRepository.UpdateAsync(resource);
        }
    }
}
