using Microsoft.AspNetCore.Mvc;
using PropertyRental.Application.DTOs;
using PropertyRental.Application.Interfaces;

namespace PropertyRental.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly IPropertyService _propertyService;

        public PropertiesController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PropertyDto>>> GetProperties(
            [FromQuery] string? city = null,
            [FromQuery] bool? isAvailable = null)
        {
            var properties = await _propertyService.GetAllPropertiesAsync(city, isAvailable);
            return Ok(properties);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PropertyDto>> GetProperty(int id)
        {
            var property = await _propertyService.GetPropertyByIdAsync(id);
            if (property == null)
                return NotFound();

            return Ok(property);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateProperty([FromBody] CreatePropertyDto dto)
        {
            var propertyId = await _propertyService.CreatePropertyAsync(dto);
            return CreatedAtAction(nameof(GetProperty), new { id = propertyId }, propertyId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProperty(int id, [FromBody] UpdatePropertyDto dto)
        {
            try
            {
                await _propertyService.UpdatePropertyAsync(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProperty(int id)
        {
            try
            {
                await _propertyService.DeletePropertyAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
