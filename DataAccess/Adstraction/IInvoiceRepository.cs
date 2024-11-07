using BadmintonBlast.Models.Dtos;
using BadmintonBlast.Models.Entities;

namespace BadmintonBlast.DataAccess.Adstraction
{
    public interface IInvoiceRepository : IRepository<Invoice>
    {
        Task<IEnumerable<InvoiceDTO>> GetAsync(
           DateTime? DateStart,
           DateTime? DateEnd,
           int pageIndex,
           int pageSize,
           string?  Customername);

        Task<int> GetTotalAsync(DateTime? DateStart, DateTime? DateEnd, string? Customername);

        Task<StatisticalInvoice> GetStatisticalInVoiceAsync(DateTime DateStart, DateTime? DateEnd);
    }
}
