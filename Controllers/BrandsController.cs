
using Microsoft.AspNetCore.Mvc;
using BadmintonBlast.Services.Adstraction;
using BadmintonBlast.Models.Dtos;
using BadmintonBlast.Services;
using System.Net;

namespace BadmintonBlast.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandsController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        // GET: api/brands
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BrandDTO>>> GetAsync()
        {
            var brands = await _brandService.GetAsync();
            return Ok(brands);
        }

        // GET: api/brands/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<BrandDTO>> GetByIdAsync([FromRoute] int id)
        {
            var brand = await _brandService.GetByIdAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            return Ok(brand);
        }

        // POST: api/brands
        [HttpPost("Insert")]
        public async Task<ActionResult> InsertAsync([FromForm] BrandDTO brandDTO)
        {
            try
            {
                await _brandService.InsertAsync(brandDTO);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // DELETE: api/brands/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _brandService.DeleteAsync(id);
            return NoContent();
        }


        [HttpPut]
        public async Task<IActionResult> UpdateAsync(int id,[FromForm] BrandDTO brandDTO)
        {
            if (brandDTO == null)
            {
                return BadRequest("Brand data is required.");
            }

            try
            {
                await _brandService.UpdateAsync(id,brandDTO); // Ensure that UpdateAsync is the correct method name in your service
                return NoContent(); // 204 No Content on successful update
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
