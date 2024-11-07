using Microsoft.AspNetCore.Mvc;
using System.Net;
using BadmintonBlast.Models.Dtos;
using BadmintonBlast.Services.Adstraction;
using BadmintonBlast.RequestModels;

namespace BadmintonBlast.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }

        // GET: api/carts
        [HttpPost("GetPaged")]
        public async Task<ActionResult<IEnumerable<CartDTO>>> GetPagedAsync([FromForm] PageRequest pageRequest)
        {
            if (pageRequest == null)
            {
                return BadRequest("Page request data is required.");
            }

            try
            {
                var carts = await _cartService.GetAsync(pageRequest);
                return Ok(carts);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpGet("getTotalCarts")]
        public async Task<ActionResult<int>> GetTotalItems()
        {
            return await _cartService.GetTotalItemsAsync();
        }

        // GET: api/carts/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CartDTO>> GetByIdAsync([FromRoute] int id)
        {
            if (id<0)
            {
                return BadRequest("ID cannot be null or empty.");
            }

            var cart = await _cartService.GetByIdAsync(id);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }

        // POST: api/carts
        [HttpPost("Insert")]
        public async Task<ActionResult> InsertAsync([FromBody] CartDTO cartDTO)
        {
            try
            {
                await _cartService.InsertAsync(cartDTO);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // DELETE: api/carts/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _cartService.DeleteAsync(id);
            return NoContent();
        }

        // PUT: api/carts/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] CartDTO cartDTO)
        {
            if (cartDTO == null)
            {
                return BadRequest("Cart data is required.");
            }

            try
            {
                await _cartService.UpdateAsync(id, cartDTO);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
