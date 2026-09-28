using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniBooking.Infrastructure.Persistence;

namespace UniBooking.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TenantsController : ControllerBase
{
    private readonly AppDbContext _context;

    public TenantsController(AppDbContext context) => _context = context;

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<List<TenantDto>>> GetAll()
    {
        var tenants = await _context.Tenants
            .Select(t => new TenantDto(t.Id, t.Name))
            .ToListAsync();

        return Ok(tenants);
    }
}

public record TenantDto(Guid Id, string Name);