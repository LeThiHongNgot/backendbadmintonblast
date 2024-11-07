using BadmintonBlast.Models.Dtos;
using BadmintonBlast.Models.Entities;

namespace BadmintonBlast.DataAccess.Adstraction
{
    public interface IBillsRepository : IRepository<Bill>
    {
        Task<IEnumerable<BillDTO>> GetAsync(
           DateTime? DateStart,
           DateTime? DateEnd,
           int pageIndex,
           int pageSize,
           int status,
           string keyword);

        Task<int> GetTotalAsync(DateTime? DateStart, DateTime? DateEnd, int Status , string keyword );

        Task<StatisticalResult> GetStatisticalBillAsync(DateTime DateStart, DateTime? DateEnd);
        Task<List<ProductSalesDTO>> GetTotalUniqueProductsSoldAsync(DateTime DateStart, DateTime? DateEnd);
    }
}
