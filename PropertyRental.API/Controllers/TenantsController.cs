using Microsoft.AspNetCore.Mvc;
using PropertyRental.Application.DTOs;
using PropertyRental.Application.Interfaces;

namespace PropertyRental.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantsController : ControllerBase
    {
        private readonly ITenantService _tenantService;

        public TenantsController(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TenantDto>>> GetTenants()
        {
            var tenants = await _tenantService.GetAllTenantsAsync();
            return Ok(tenants);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TenantDto>> GetTenant(int id)
        {
            var tenant = await _tenantService.GetTenantByIdAsync(id);
            if (tenant == null)
                return NotFound();

            return Ok(tenant);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateTenant([FromBody] CreateTenantDto dto)
        {
            var tenantId = await _tenantService.CreateTenantAsync(dto);
            return CreatedAtAction(nameof(GetTenant), new { id = tenantId }, tenantId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTenant(int id, [FromBody] UpdateTenantDto dto)
        {
            try
            {
                await _tenantService.UpdateTenantAsync(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTenant(int id)
        {
            try
            {
                await _tenantService.DeleteTenantAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
