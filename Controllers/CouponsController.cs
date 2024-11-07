using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BadmintonBlast.Models.Entities;
using BadmintonBlast.Models.Dtos;
using BadmintonBlast.Services.Adstraction;
using System.Net;

namespace BadmintonBlast.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponsController : ControllerBase
    {
        private readonly ICouponService _couponService;

        public CouponsController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        // GET: api/coupons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CouponDTO>>> GetAsync()
        {
            var coupons = await _couponService.GetAsync();
            return Ok(coupons);
        }

        // GET: api/coupons/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CouponDTO>> GetByIdAsync([FromRoute] int id)
        {
            var coupon = await _couponService.GetByIdAsync(id);
            if (coupon == null)
            {
                return NotFound();
            }
            return Ok(coupon);
        }

        // POST: api/coupons
        [HttpPost("Insert")]
        public async Task<ActionResult> InsertAsync([FromBody] CouponDTO couponDTO)
        {
            try
            {
                await _couponService.InsertAsync(couponDTO);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // DELETE: api/coupons/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _couponService.DeleteAsync(id);
            return NoContent();
        }

        // PUT: api/coupons/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] CouponDTO couponDTO)
        {
            if (couponDTO == null)
            {
                return BadRequest("Coupon data is required.");
            }

            try
            {
                await _couponService.UpdateAsync(id, couponDTO);
                return NoContent(); // 204 No Content on successful update
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
