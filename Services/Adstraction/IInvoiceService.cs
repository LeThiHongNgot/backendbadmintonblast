using BadmintonBlast.Models.Dtos;
using BadmintonBlast.RequestModels;

namespace BadmintonBlast.Services.Adstraction
{
    public interface IInvoiceService
    {
        Task<IEnumerable<InvoiceDTO>> GetInvoiceAsync(GetInvoiceRequest model);
        Task<int> GetTotalInvoiceAsync(GetTotalInvoiceRequest model);
        Task<InvoiceDTO> GetByIdAsync(int id);
        Task<int> InsertAsync(InvoiceDTO billDTO);
        Task UpdateAsync(int id, InvoiceDTO invoiceDTO);
        Task DeleteAsync(int id);

        Task<StatisticalInvoice> GetStatisticalInVoiceAsync(DateTime DateStart, DateTime? DateEnd);
    }
}
