using BadmintonBlast.Models.Dtos;
using BadmintonBlast.Services.Adstraction;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BadmintonBlast.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductStockController : ControllerBase
    {
        private readonly IProductStockService productStockService;

        public ProductStockController(IProductStockService productStockService)
        {
            this.productStockService = productStockService;
        }

        // GET: api/ProductStock
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductStockDTO>>> GetAllProductStocks()
        {
            var productStocks = await productStockService.GetAllAsync();
            return Ok(productStocks);
        }

        // GET: api/ProductStock/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductStockDTO>> GetProductStockById(int id)
        {
            var productStock = await productStockService.GetByIdAsync(id);
            if (productStock == null)
            {
                return NotFound();
            }
            return Ok(productStock);
        }

        // GET: api/ProductStock/Product/{productId}
        [HttpGet("Product/{productId}")]
        public async Task<ActionResult<IEnumerable<ProductStockDTO>>> GetProductStocksByProductId(int productId)
        {
            var productStocks = await productStockService.GetByProductIdAsync(productId);
            if (productStocks == null || !productStocks.Any())
            {
                return NotFound();
            }
            return Ok(productStocks);
        }

        // POST: api/ProductStock
        [HttpPost]
        public async Task<ActionResult> InsertProductStock([FromBody] ProductStockDTO productStockDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await productStockService.InsertAsync(productStockDTO);
            return CreatedAtAction(nameof(GetProductStockById), new { id = productStockDTO.Idproduct }, productStockDTO);
        }

        // PUT: api/ProductStock/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProductStock(int id, [FromBody] ProductStockDTO productStockDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await productStockService.UpdateAsync(id, productStockDTO);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // DELETE: api/ProductStock/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProductStockById(int id)
        {
            try
            {
                await productStockService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // DELETE: api/ProductStock/Product/{productId}
        [HttpDelete("Product/{productId}")]
        public async Task<ActionResult> DeleteProductStocksByProductId(int productId)
        {
            try
            {
                await productStockService.DeleteByProductIdAsync(productId);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
