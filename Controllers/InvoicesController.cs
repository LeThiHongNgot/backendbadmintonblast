using BadmintonBlast.Models.Dtos;
using BadmintonBlast.RequestModels;
using BadmintonBlast.Services;
using BadmintonBlast.Services.Adstraction;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BadmintonBlast.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            this.invoiceService = invoiceService;
        }

        // Get invoice list with filtering and pagination
        [HttpGet]
        public async Task<IActionResult> GetInvoices([FromQuery] GetInvoiceRequest request)
        {
            var invoices = await invoiceService.GetInvoiceAsync(request);
            return Ok(invoices);
        }

        // Get total invoice count with filtering
        [HttpGet("total")]
        public async Task<IActionResult> GetTotalInvoices([FromQuery] GetTotalInvoiceRequest request)
        {
            var totalInvoices = await invoiceService.GetTotalInvoiceAsync(request);
            return Ok(totalInvoices);
        }

        // Get a specific invoice by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInvoiceById(int id)
        {
            var invoice = await invoiceService.GetByIdAsync(id);
            if (invoice == null)
                return NotFound($"Invoice with ID {id} not found.");

            return Ok(invoice);
        }

        // Create a new invoice
        [HttpPost]
        public async Task<IActionResult> CreateInvoice([FromBody] InvoiceDTO invoiceDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newInvoiceId = await invoiceService.InsertAsync(invoiceDTO);
            return CreatedAtAction(nameof(GetInvoiceById), new { id = newInvoiceId }, invoiceDTO);
        }

        // Update an existing invoice
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInvoice(int id, [FromBody] InvoiceDTO invoiceDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await invoiceService.UpdateAsync(id, invoiceDTO);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // Delete an invoice by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            try
            {
                await invoiceService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Invoice with ID {id} not found.");
            }
        }

        [HttpGet("statisticsInvoice")]
        public async Task<IActionResult> statisticsInvoice(DateTime DateStart, DateTime? DateEnd)
        {
            var result = await invoiceService.GetStatisticalInVoiceAsync(DateStart,DateEnd);
            return Ok(result);
        }
    }
}
