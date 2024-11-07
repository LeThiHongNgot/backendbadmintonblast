using AutoMapper;
using BadmintonBlast.DataAccess.Adstraction;
using BadmintonBlast.Models.Dtos;
using BadmintonBlast.Models.Entities;
using BadmintonBlast.RequestModels;
using BadmintonBlast.Services.Adstraction;

namespace BadmintonBlast.Services
{
    public class InvoiceService :   IInvoiceService
    {

        private readonly IUnitOfWork context;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;

        public InvoiceService(
            IUnitOfWork context,
            IWebHostEnvironment webHostEnvironment,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor
        )
        {
            this.context = context;
            this.webHostEnvironment = webHostEnvironment;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<InvoiceDTO>> GetInvoiceAsync(GetInvoiceRequest model)
        {
            return await context.InvoiceRepository.GetAsync(model.DateStart, model.DateEnd, model.PageIndex, model.PageSize, model.CustomerName);


        }

        public async Task<int> GetTotalInvoiceAsync(GetTotalInvoiceRequest model)
        {
            return await context.InvoiceRepository.GetTotalAsync(model.DateStart, model.DateEnd, model.CustomerName);

        }

        // Get invoice by ID
        public async Task<InvoiceDTO> GetByIdAsync(int id)
        {
            var invoice = await context.InvoiceRepository.GetSingleAsync(i => i.Idinvoice == id);

            if (invoice == null)
                return null;

            return mapper.Map<InvoiceDTO>(invoice);
        }

        // Insert a new invoice
        public async Task<int> InsertAsync(InvoiceDTO invoiceDTO)
        {
            if (invoiceDTO == null)
                throw new ArgumentNullException(nameof(invoiceDTO));

            var entity = mapper.Map<Invoice>(invoiceDTO);

            await context.InvoiceRepository.InsertAsync(entity);
            await context.SaveChangesAsync();

            return entity.Idinvoice;
        }

        // Update an existing invoice
        public async Task UpdateAsync(int id, InvoiceDTO invoiceDTO)
        {
            if (invoiceDTO == null)
                throw new ArgumentNullException(nameof(invoiceDTO));

            var existingEntity = await context.InvoiceRepository.GetSingleAsync(id);

            if (existingEntity == null)
                throw new KeyNotFoundException($"Invoice with ID {id} not found.");

            // Map updated values to existing entity
            mapper.Map(invoiceDTO, existingEntity);

            context.InvoiceRepository.Update(existingEntity);
            await context.SaveChangesAsync();
        }

        // Delete an invoice by ID
        public async Task DeleteAsync(int id)
        {
            await context.InvoiceRepository.DeleteAsync(id);
            await context.SaveChangesAsync();
        }

        public async Task<StatisticalInvoice> GetStatisticalInVoiceAsync(DateTime DateStart, DateTime? DateEnd)
        {
            return await context.InvoiceRepository.GetStatisticalInVoiceAsync(DateStart, DateEnd);
        }

    }
}
