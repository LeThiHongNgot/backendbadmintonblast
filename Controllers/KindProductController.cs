using Microsoft.AspNetCore.Mvc;
using BadmintonBlast.Services.Adstraction;
using BadmintonBlast.Models.Dtos;
using System.Net;

namespace BadmintonBlast.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KindProductsController : ControllerBase
    {
        private readonly IKindProductService _kindProductService;

        public KindProductsController(IKindProductService kindProductService)
        {
            _kindProductService = kindProductService;
        }

        // GET: api/kindproducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KindProductDTO>>> GetAsync()
        {
            var kindProducts = await _kindProductService.GetAsync();
            return Ok(kindProducts);
        }

        // GET: api/kindproducts/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<KindProductDTO>> GetByIdAsync([FromRoute] int id)
        {
            var kindProduct = await _kindProductService.GetByIdAsync(id);
            if (kindProduct == null)
            {
                return NotFound();
            }
            return Ok(kindProduct);
        }

        // POST: api/kindproducts
        [HttpPost("Insert")]
        public async Task<ActionResult> InsertAsync([FromForm] KindProductDTO kindProductDTO)
        {
            try
            {
                await _kindProductService.InsertAsync(kindProductDTO);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // PUT: api/kindproducts/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] KindProductDTO kindProductDTO)
        {
            if (kindProductDTO == null)
            {
                return BadRequest("KindProduct data is required.");
            }

            try
            {
                await _kindProductService.UpdateAsync(id, kindProductDTO);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/kindproducts/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _kindProductService.DeleteAsync(id);
            return NoContent();
        }
    }
}
