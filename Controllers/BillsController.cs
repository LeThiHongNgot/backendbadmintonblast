using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BadmintonBlast.Models.Entities;
using BadmintonBlast.Models.Dtos;
using BadmintonBlast.RequestModels;
using System.Net;
using BadmintonBlast.Services.Adstraction;
using BadmintonBlast.Services;

namespace BadmintonBlast.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillsController : ControllerBase
    {
        private readonly IBillService _billService;

        public BillsController(IBillService billService)
        {
            _billService = billService;
        }
        // GET: api/bills
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BillDTO>>> GetAsync([FromQuery] GetBillRequest query)
        {
            var bills = await _billService.GetBillsAsync(query);
            return Ok(bills);
        }
        [HttpGet("customer/{idCustomer}")]
        public async Task<ActionResult<IEnumerable<BillDTO>>> GetCustomerAsync([FromRoute] int idCustomer )
        {
            var bills = await _billService.GetByCustomerIdAsync(idCustomer);
            return Ok(bills);
        }

        // GET: api/bills/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<BillDTO>> GetByIdAsync([FromRoute] int id)
        {
            var bill = await _billService.GetByIdAsync(id);
            if (bill == null)
            {
                return NotFound();
            }
            return Ok(bill);
        }
        [HttpGet("GetTotalBill")]
        public async Task<ActionResult<int>> GetTotalItems([FromQuery] GetTotalBillRequest request)
        {
            try
            {
                var totalItems = await _billService.GetTotalBillAsync(request);
                return Ok(totalItems);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);


            }
        }

                // POST: api/bills
                [HttpPost("Insert")]
        public async Task<ActionResult> InsertAsync([FromBody] BillDTO billDTO)
        {
            try
            {
                // Gọi hàm InsertAsync để chèn Bill và lấy Idbill
                var idBill = await _billService.InsertAsync(billDTO);

                // Trả về Ok với Id của Bill
                return Ok(new { idBill });
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }


        // DELETE: api/bills/{id}
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await _billService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }


        // PUT: api/bills/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] byte status)
        {
            try
            {
                // Cập nhật trạng thái hóa đơn thông qua service
                await _billService.UpdateAsync(id, status);
                return NoContent(); // Trả về 204 No Content khi cập nhật thành công
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Bill with id {id} not found."); // Trả về 404 nếu không tìm thấy hóa đơn
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}"); // Xử lý lỗi 500 nếu có lỗi
            }
        }
        [HttpGet("statistics")]
        public async Task<IActionResult> GetBillStatistics(DateTime DateStart, DateTime? DateEnd)
        {
            var result = await _billService.GetStatisticalBillAsync(DateStart, DateEnd);
            return Ok(result);
        }
        [HttpGet("GetTotalUniqueProducts")]
        public async Task<IActionResult> GetTotalUniqueProducts(DateTime DateStart, DateTime? DateEnd)
        {
            var result = await _billService.GetTotalUniqueProductsSoldAsync(DateStart, DateEnd);
            return Ok(result);
        }
    }
}
