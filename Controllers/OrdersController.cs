using Microsoft.AspNetCore.Mvc;
using BadmintonBlast.Services.Adstraction;
using BadmintonBlast.Models.Dtos;
using System.Net;

namespace BadmintonBlast.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: api/orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetAsync()
        {
            var orders = await _orderService.GetAsync();
            return Ok(orders);
        }

        // GET: api/orders/{id}
        [HttpGet("{idBill}")]
        public async Task<ActionResult<OrderDTO>> GetByIdAsync([FromRoute] int idBill)
        {
            var order = await _orderService.GetByIdAsync(idBill);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        // POST: api/orders
        [HttpPost("Insert")]
        public async Task<ActionResult> InsertAsync([FromBody] OrderDTO orderDTO)
        {
            try
            {
                await _orderService.InsertAsync(orderDTO);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // DELETE: api/orders/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _orderService.DeleteAsync(id);
            return NoContent();
        }

        // PUT: api/orders/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] OrderDTO orderDTO)
        {
            if (orderDTO == null)
            {
                return BadRequest("Order data is required.");
            }

            try
            {
                await _orderService.UpdateAsync(id, orderDTO); // Ensure that UpdateAsync is the correct method name in your service
                return NoContent(); // 204 No Content on successful update
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
