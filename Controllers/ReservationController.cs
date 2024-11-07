using Microsoft.AspNetCore.Mvc;
using BadmintonBlast.Services.Adstraction;
using BadmintonBlast.Models.Dtos;
using System.Net;

namespace BadmintonBlast.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationsController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        // GET: api/reservations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetAsync()
        {
            var reservations = await _reservationService.GetAsync();
            return Ok(reservations);
        }

        // GET: api/reservations/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationDTO>> GetByIdAsync([FromRoute] int id)
        {
            var reservation = await _reservationService.GetByInvoiceIdAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            return Ok(reservation);
        }

        // POST: api/reservations
        [HttpPost("Insert")]
        public async Task<ActionResult> InsertAsync([FromBody] ReservationDTO reservationDTO)
        {
            try
            {
                await _reservationService.InsertAsync(reservationDTO);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // DELETE: api/reservations/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _reservationService.DeleteAsync(id);
            return NoContent();
        }

        // PUT: api/reservations/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] ReservationDTO reservationDTO)
        {
            if (reservationDTO == null)
            {
                return BadRequest("Reservation data is required.");
            }

            try
            {
                await _reservationService.UpdateAsync(id, reservationDTO);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
