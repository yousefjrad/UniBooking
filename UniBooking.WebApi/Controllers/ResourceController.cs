using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniBooking.Application.Features.Resources.Dtos;
using UniBooking.Application.Features.Resources;
using UniBooking.Domain.Entities;
using System.Security.Claims;
using UniBooking.Application.Features.Bookings.DTOs;

namespace UniBooking.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ResourcesController : ControllerBase
{
    private readonly ResourceService _resourceService;

    public ResourcesController(ResourceService resourceService) =>
        _resourceService = resourceService;

    private Guid CurrentTenantId =>
        Guid.Parse(User.FindFirstValue("TenantId")!);

    [HttpPost("Create")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ResourceDto>> Create(CreateResourceDto dto)
    {
        var result = await _resourceService.CreateAsync(dto, CurrentTenantId);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet]
    public async Task<ActionResult<List<ResourceDto>>> GetAll()
    {
        var result = await _resourceService.GetAllAsync(CurrentTenantId);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ResourceDto>> GetById(Guid id)
    {
        var result = await _resourceService.GetByIdAsync(id, CurrentTenantId);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpGet("available")]
    public async Task<ActionResult<List<ResourceDto>>> GetAvailable([FromQuery] int minCapacity = 1)
    {
        var result = await _resourceService.GetAvailableAsync(CurrentTenantId, minCapacity);
        return Ok(result);
    }


    [HttpPut("{id:guid}/Update")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(Guid ResourceId , UpdateResourceDto dto)
    {
       await _resourceService.UpdateAsync(ResourceId , dto , CurrentTenantId);
        return NoContent();
    }


    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid ResourceId)
    {
        await _resourceService.DeleteAsync(ResourceId, CurrentTenantId);
        return NoContent();
    }
}