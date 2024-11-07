using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BadmintonBlast.Models;
using BadmintonBlast.Services.Adstraction;
using BadmintonBlast.Models.Dtos;
using BadmintonBlast.RequestModels;
using BadmintonBlast.Services;
using System.Net;
using Azure.Core;

namespace BadmintonBlast.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("getProduct")]
        public async Task<ActionResult<IEnumerable<ProductsDTO>>> Get([FromQuery] GetProductRequest query)
        {
            try
            {
                // Fetch the list of customers from the service
                var product = await _productService.GetProductAsync(query);

                // Return the result wrapped in an ActionResult with HTTP 200 OK status
                return Ok(product);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<ProductsDTO>> GetById([FromRoute] int id)
        {
            try
            {
                var result = await _productService.GetByIdAsync(id);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpGet("GetTotalProduct")]
        public async Task<ActionResult<int>> GetTotalItems([FromQuery] GetTotalProductRequest request)
        {
            try
            {
                var totalItems = await _productService.GetTotalProductAsync(request);
                return Ok(totalItems);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPost("Insert")]
        public async Task<ActionResult> Insert([FromForm] UpdateProductImageRequest request)
        {
            try
            {
                await _productService.InsertAsync(request);

                // Assuming 'request.Idproduct' is the ID of the inserted product
                return CreatedAtAction(nameof(GetById), new { id = request.Idproduct }, request);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPut("Update")]
        public async Task<ActionResult> Update(int id, [FromForm] UpdateProductImageRequest request)
        {
               if (id != request.Idproduct)
            {
                return BadRequest("ID mismatch");
            }
            try
            {
                await _productService.UpdateAsync(id, request);

                // Assuming 'request.Idproduct' is the ID of the inserted product
                return CreatedAtAction(nameof(GetById), new { id = request.Idproduct }, request);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }




        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await _productService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

    }
}
