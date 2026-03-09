using Microsoft.AspNetCore.Mvc;
using PropertyRental.Application.DTOs;
using PropertyRental.Application.Interfaces;

namespace PropertyRental.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeasesController : ControllerBase
    {
        private readonly ILeaseService _leaseService;

        public LeasesController(ILeaseService leaseService)
        {
            _leaseService = leaseService;
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateLease([FromBody] CreateLeaseDto dto)
        {
            try
            {
                var leaseId = await _leaseService.CreateLeaseAsync(dto);
                return Ok(leaseId);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("property/{propertyId}")]
        public async Task<ActionResult<IEnumerable<LeaseDto>>> GetLeasesByProperty(int propertyId)
        {
            var leases = await _leaseService.GetLeasesByPropertyIdAsync(propertyId);
            return Ok(leases);
        }

        [HttpPut("end/{id}")]
        public async Task<IActionResult> EndLease(int id)
        {
            try
            {
                await _leaseService.EndLeaseAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}