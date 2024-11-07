using AutoMapper;
using BadmintonBlast.DataAccess.Adstraction;
using BadmintonBlast.Models.Dtos;
using BadmintonBlast.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BadmintonBlast.DataAccess
{
    public class BillRepository : Repository<Bill>, IBillsRepository
    {
        public BillRepository(DbContext context, IMapper mapper)
            : base(context, mapper) { }
        public async Task<IEnumerable<BillDTO>> GetAsync(
         DateTime? dateStart,
         DateTime? dateEnd,
         int pageIndex,
         int pageSize,
         int status,
         string keyword)
        {
            IQueryable<Bill> query = context.Set<Bill>();

            // Lọc theo khoảng thời gian nếu có
            if (dateStart.HasValue && dateEnd.HasValue)
            {
                query = query.Where(bill => bill.Dateorder >= dateStart.Value && bill.Dateorder <= dateEnd.Value);
            }

            // Lọc theo trạng thái
            if (status > 0)
            {
                query = query.Where(bill => bill.Status == status);
            }
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(bill => bill.Namecustomer.Contains(keyword)); // Giả sử Customer là liên kết tới bảng khách hàng
            }

            // Phân trang
            query = query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize);

            // Trả về danh sách DTOs
            return await mapper.ProjectTo<BillDTO>(query).ToListAsync();
        }

        // Phương thức tính tổng số hóa đơn theo các tiêu chí lọc
        public async Task<int> GetTotalAsync(DateTime? dateStart, DateTime? dateEnd, int status, string keyword)
        {
            IQueryable<Bill> query = context.Set<Bill>();

            // Lọc theo khoảng thời gian nếu có
            if (dateStart.HasValue && dateEnd.HasValue)
            {
                query = query.Where(bill => bill.Dateorder >= dateStart.Value && bill.Dateorder <= dateEnd.Value);
            }

            // Lọc theo trạng thái
            if (status > 0)
            {
                query = query.Where(bill => bill.Status == status);
            }
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(bill => bill.Namecustomer.Contains(keyword)); // Giả sử Customer là liên kết tới bảng khách hàng
            }
            // Trả về tổng số hóa đơn phù hợp
            return await query.CountAsync();
        }

        public async Task<StatisticalResult> GetStatisticalBillAsync(DateTime DateStart, DateTime? DateEnd)
        {
            DateTime dateEndValue = DateEnd ?? DateTime.Now;

            // Total number of orders within the date range
            var totalBills = await context.Set<Bill>()
                .Where(o => o.Dateorder >= DateStart && o.Dateorder <= dateEndValue)
                .CountAsync();

            // Number of completed orders (Paid or Delivered)
            var completedBills = await context.Set<Bill>()
                .Where(o => o.Dateorder >= DateStart && o.Dateorder <= dateEndValue &&
                            (o.Status == 905 || o.Pay == "Đã Thanh Toán"))
                .CountAsync();

            // Total revenue from Paid or Delivered orders
            var totalRevenue = await context.Set<Bill>()
                .Where(o => o.Dateorder >= DateStart && o.Dateorder <= dateEndValue &&
                            (o.Status == 905 || o.Pay == "Đã Thanh Toán"))
                .SumAsync(o => (int?)o.Totalamount ?? 0);  // Ensure null values are handled

            // Return the statistical result
            return new StatisticalResult
            {
                TotalBills = totalBills,
                CompletedBills = completedBills,
                TotalRevenue = totalRevenue,
            };
        }

        public async Task<List<ProductSalesDTO>> GetTotalUniqueProductsSoldAsync(DateTime DateStart, DateTime? DateEnd)
        {
            DateTime dateEndValue = DateEnd ?? DateTime.Now;

            var productSales = await context.Set<Order>()
                .Where(o => o.DateOrder >= DateStart && o.DateOrder <= dateEndValue)
                .GroupBy(o => o.Idproduct)
                .Select(g => new ProductSalesDTO
                {
                    ProductId = g.Key,
                    TotalAmount = g.Sum(x => (decimal?)(x.Price * x.Quatity) ?? 0m), // Ensure null values are handled
                    TotalQuantity = g.Sum(x => (int?)x.Quatity ?? 0) // Ensure null values are handled
                })
                .ToListAsync();

            return productSales;
        }


    }

}

