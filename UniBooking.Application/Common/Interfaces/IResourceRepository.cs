using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniBooking.Application.Features.Resources.Dtos;
using UniBooking.Domain.Entities;

namespace UniBooking.Application.Common.Interfaces
{
    public interface IResourceRepository
    {
        Task<Resource?> GetByIdAsync(Guid id , Guid tenantId);
        Task<List<Resource>> GetAllAsync(Guid tenantId);

        Task<List<Resource>> GetAvailableAsync(Guid tenantId, int minCapacity);

        Task AddAsync (Resource resource);

        Task UpdateAsync (Resource resource);

        
    }
}
