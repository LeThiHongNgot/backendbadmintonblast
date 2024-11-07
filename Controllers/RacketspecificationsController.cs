using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BadmintonBlast.Models;
using BadmintonBlast.Models.Dtos;
using BadmintonBlast.Services;
using System.Net;

namespace BadmintonBlast.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RacketspecificationsController : ControllerBase
    {
        private readonly IRacketspecificationService _racketspecificationService;

        public RacketspecificationsController(IRacketspecificationService racketspecificationService)
        {
            _racketspecificationService = racketspecificationService;
        }

        // GET: api/racketspecification
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RacketspecificationDTO>>> GetAsync()
        {
            var racketspecifications = await _racketspecificationService.GetAsync();
            return Ok(racketspecifications);
        }

        // GET: api/racketspecification/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<RacketspecificationDTO>> GetByIdAsync([FromRoute] int id)
        {
            var racketspecification = await _racketspecificationService.GetByIdAsync(id);
            if (racketspecification == null)
            {
                return NotFound();
            }
            return Ok(racketspecification);
        }

        // POST: api/racketspecification
        [HttpPost]
        public async Task<ActionResult> InsertAsync([FromBody] RacketspecificationDTO racketspecificationDTO)
        {
            try
            {
                await _racketspecificationService.InsertAsync(racketspecificationDTO);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // PUT: api/racketspecification/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] RacketspecificationDTO racketspecificationDTO)
        {
            if (racketspecificationDTO == null)
            {
                return BadRequest("Racket specification data is required.");
            }

            try
            {
                await _racketspecificationService.UpdateAsync(id, racketspecificationDTO);
                return NoContent(); // 204 No Content on successful update
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
