using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UniBooking.Application.Common.Interfaces;
using UniBooking.Domain.Entities;

namespace UniBooking.Infrastructure.Persistence.Repositories
{
    public class ResourcesRepository : IResourceRepository
    {
        private readonly AppDbContext _context;

        public ResourcesRepository(AppDbContext context)=> _context = context;


        public async Task<Resource?> GetByIdAsync(Guid id, Guid tenantId) =>
        await _context.Resources
        .FirstOrDefaultAsync(r => r.Id == id && r.TenantId == tenantId);

        public async Task<List<Resource>> GetAllAsync(Guid tenantId)=>
            await _context.Resources.AsNoTracking().Where(x => x.TenantId == tenantId && x.IsActive).ToListAsync();

        public async Task<List<Resource>> GetAvailableAsync(Guid tenantId, int minCapacity) =>
         await _context.Resources.AsNoTracking()
             .Where(r => r.TenantId == tenantId && r.IsActive && r.Capacity >= minCapacity)
             .ToListAsync();

        public async Task AddAsync (Resource resource)
        {
            await _context.Resources.AddAsync(resource);

            await _context.SaveChangesAsync();
        }
    }
}
