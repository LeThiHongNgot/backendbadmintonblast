using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BadmintonBlast.Models.Dtos;
using BadmintonBlast.Services.Adstraction;
using System.Net;
using BadmintonBlast.RequestModels;

namespace BadmintonBlast.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreviewsController : ControllerBase
    {
        private readonly IPreviewService _previewService;

        public PreviewsController(IPreviewService previewService)
        {
            _previewService = previewService;
        }

        // GET: api/previews
        [HttpPost("GetPaged")]
        public async Task<ActionResult<IEnumerable<PreviewDTO>>> GetPagedAsync([FromForm] PageRequest pageRequest)
        {
            if (pageRequest == null)
            {
                return BadRequest("Page request data is required.");
            }

            try
            {
                var previews = await _previewService.GetAsync(pageRequest);
                return Ok(previews);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpGet("getTotalPreview")]
        public async Task<ActionResult<int>> GetGetTotalItems()
        {
            return await _previewService.GetTotalItemsAsync();
        }

        // GET: api/previews/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PreviewDTO>> GetByIdAsync([FromRoute] int id)
        {
            if (id<0)
            {
                return BadRequest("ID cannot be null or empty.");
            }

            var preview = await _previewService.GetByIdAsync(id);
            if (preview == null)
            {
                return NotFound();
            }
            return Ok(preview);
        }

        // POST: api/previews
        [HttpPost("Insert")]
        public async Task<ActionResult> InsertAsync([FromBody] PreviewDTO previewDTO)
        {
            try
            {
                await _previewService.InsertAsync(previewDTO);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // DELETE: api/previews/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _previewService.DeleteAsync(id);
            return NoContent();
        }

        // PUT: api/previews/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] PreviewDTO previewDTO)
        {
            if (previewDTO == null)
            {
                return BadRequest("Preview data is required.");
            }

            try
            {
                await _previewService.UpdateAsync( id, previewDTO); // Ensure that UpdateAsync is the correct method name in your service
                return NoContent(); // 204 No Content on successful update
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
