using BadmintonBlast.Models.Dtos;
using BadmintonBlast.RequestModels;

namespace BadmintonBlast.Services.Adstraction
{
    public interface IBillService
    {
        Task<IEnumerable<BillDTO>> GetBillsAsync(GetBillRequest model);
        Task<int> GetTotalBillAsync(GetTotalBillRequest model);
        Task<BillDTO> GetByIdAsync(int id);
        Task<IEnumerable<BillDTO>> GetByCustomerIdAsync(int customerId);
        Task<int> InsertAsync(BillDTO billDTO);
        Task UpdateAsync(int id, byte status);
        Task DeleteAsync(int id);
        Task<StatisticalResult> GetStatisticalBillAsync(DateTime DateStart, DateTime? DateEnd);
        Task<List<ProductSalesDTO>> GetTotalUniqueProductsSoldAsync(DateTime DateStart, DateTime? DateEnd);
    }
}
