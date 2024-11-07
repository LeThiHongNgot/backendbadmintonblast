using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BadmintonBlast.Models;
using System.Net;
using BadmintonBlast.RequestModels;
using BadmintonBlast.Models.Dtos;
using BadmintonBlast.Services.Adstraction;
using Microsoft.AspNetCore.Identity.Data;

namespace BadmintonBlast.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }


        [HttpGet("GetCustomers")]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> Get([FromQuery] GetCustomerRequest query)
        {

            try
            {

                var customers = await _customerService.GetCustomersAsync(query);

                return Ok(customers);
            }
            catch (Exception e)
            {
                // Return a 500 Internal Server Error with the exception message
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestCustomer request)
        {
            try
            {
                var token = await _customerService.LoginAsync(request.Email, request.Password);
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Logged In Successfully",
                    Token = token
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }
        }
   

    [HttpGet("GetById/{id}")]
        public async Task<ActionResult<CustomerDTO>> GetById([FromRoute] int id)
        {
            try
            {
                var result = await _customerService.GetByIdAsync(id);
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

        [HttpGet("GetTotalCustomers")]
        public async Task<ActionResult<int>> GetTotalItems([FromQuery] GetTotalCustomerRequest request)
        {
            try
            {
                var totalItems = await _customerService.GetTotalCustomersAsync(request);
                return Ok(totalItems);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPost("Insert")]
        public async Task<ActionResult> Insert([FromForm] UpdateCustomerRequest customerRequest)
        {
            try
            {
                await _customerService.InsertAsync(customerRequest);
                return CreatedAtAction(nameof(GetById), new { id = customerRequest.Idcustomer }, customerRequest);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPut("Update/{id}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromForm] UpdateCustomerRequest customerRequest)
        {
         

            try
            {
                await _customerService.UpdateAsync(id,customerRequest);
                return NoContent();
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
                await _customerService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
