using AutoMapper;
using BadmintonBlast.DataAccess.Adstraction;
using BadmintonBlast.Models.Dtos;
using BadmintonBlast.Models.Entities;
using Microsoft.EntityFrameworkCore;
namespace BadmintonBlast.DataAccess
{
    public class InvoiceRepository :Repository<Invoice>,IInvoiceRepository
    {
        public InvoiceRepository(DbContext context, IMapper mapper)
            : base(context, mapper) { }
        public async Task<IEnumerable<InvoiceDTO>> GetAsync(
         DateTime? dateStart,
         DateTime? dateEnd,
         int pageIndex,
         int pageSize,
         string? Customername)
        {
            IQueryable<Invoice> query = context.Set<Invoice>();

            // Lọc theo khoảng thời gian nếu có
            if (dateStart.HasValue && dateEnd.HasValue)
            {
                query = query.Where(invoice =>invoice.Reservationdate >= dateStart.Value && invoice.Reservationdate <= dateEnd.Value);
            }

            // Lọc theo tên khách hàng
            if (!string.IsNullOrWhiteSpace(Customername))
            {
                query = query.Where(customer => customer.Customername.Contains(Customername));
            }
            // Phân trang
            query = query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize);

            // Trả về danh sách DTOs
            return await mapper.ProjectTo<InvoiceDTO>(query).ToListAsync();
        }

        // Phương thức tính tổng số hóa đơn theo các tiêu chí lọc
        public async Task<int> GetTotalAsync(DateTime? dateStart, DateTime? dateEnd, string? CustomerName)
        {
            IQueryable<Bill> query = context.Set<Bill>();

            // Lọc theo khoảng thời gian nếu có
            if (dateStart.HasValue && dateEnd.HasValue)
            {
                query = query.Where(bill => bill.Dateorder >= dateStart.Value && bill.Dateorder <= dateEnd.Value);
            }

            // Lọc theo tên khách hàng
            if (!string.IsNullOrWhiteSpace(CustomerName))
            {
                query = query.Where(customer => customer.Namecustomer.Contains(CustomerName));
            }
            // Trả về tổng số hóa đơn phù hợp
            return await query.CountAsync();
        }

        public async Task<StatisticalInvoice> GetStatisticalInVoiceAsync(DateTime DateStart, DateTime? DateEnd)
        {
            DateTime dateEndValue = DateEnd ?? DateTime.Now;

            // Total number of orders within the date range
            var totalInvoice = await context.Set<Invoice>()
                .Where(o => o.Reservationdate >= DateStart && o.Reservationdate <= dateEndValue)
                .CountAsync();

            // Number of completed orders (Paid or Delivered)
            var completedInvoice = await context.Set<Invoice>()
                .Where(o => o.Reservationdate >= DateStart && o.Reservationdate <= dateEndValue &&
                            (o.Status == true))
                .CountAsync();

            // Total revenue from Paid or Delivered orders
            var totalRevenue = await context.Set<Invoice>()
                .Where(o => o.Reservationdate >= DateStart && o.Reservationdate <= dateEndValue &&
                            (o.Status == true))
                .SumAsync(o => (int?)o.Totalamount ?? 0);  // Ensure null values are handled

            // Return the statistical result
            return new StatisticalInvoice
            {
                TotalInvoice = totalInvoice,        
                CompletedInvoice = completedInvoice,
                TotalRevenue = totalRevenue,
            };
        }

    }
}
