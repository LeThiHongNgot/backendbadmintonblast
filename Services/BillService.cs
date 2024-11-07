using AutoMapper;
using Azure.Core;
using BadmintonBlast.DataAccess;
using BadmintonBlast.DataAccess.Adstraction;
using BadmintonBlast.Helpers;
using BadmintonBlast.Models.Dtos;
using BadmintonBlast.Models.Entities;
using BadmintonBlast.RequestModels;
using BadmintonBlast.Services.Adstraction;
using Microsoft.EntityFrameworkCore;

namespace BadmintonBlast.Services
{
    public class BillService: IBillService
    {

        private readonly IUnitOfWork context;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;

        public BillService(
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

        public async Task DeleteAsync(int id)
        {
            await context.BillRepository.DeleteAsync(id);
            await context.SaveChangesAsync();
        }

        public async Task<BillDTO> GetByIdAsync(int id)
        {
            // Lấy thông tin khách hàng từ repository
            var bill = await context.BillRepository.GetSingleAsync<Bill>(id);

            if (bill == null)
            {
                throw new KeyNotFoundException("Bill not found");
            }

            // Ánh xạ thông tin khách hàng vào DTO
            var BillDto = mapper.Map<Bill, BillDTO>(bill);

            return BillDto;
        }
        public async Task<IEnumerable<BillDTO>> GetByCustomerIdAsync(int customerId)
        {
            // Lấy danh sách hóa đơn của khách hàng từ repository
            var bills = await context.BillRepository.GetAsync<Bill>(b => b.Idcustomer == customerId);

            if (bills == null)
            {
                throw new KeyNotFoundException("No bills found for the specified customer.");
            }

            // Ánh xạ danh sách hóa đơn vào DTO
            return  mapper.Map < IEnumerable<BillDTO>>(bills);

        }


        public async Task<IEnumerable<BillDTO>> GetBillsAsync(GetBillRequest model)
        {
            return await context.BillRepository.GetAsync(model.DateStart,model.DateEnd, model.PageIndex, model.PageSize, model.status, model.Keyword    );

        }

        public Task<int> GetTotalBillAsync(GetTotalBillRequest model)
        {
            return context.BillRepository.GetTotalAsync(model.DateStart, model.DateEnd, model.status ,model.Keyword);
        }

        public async Task UpdateAsync(int id, byte status)
        {
            // Lấy thông tin hóa đơn hiện tại từ cơ sở dữ liệu
            var existingBill = await context.BillRepository.GetSingleAsync<Bill>(id);

            if (existingBill == null)
            {
                throw new KeyNotFoundException("Bill not found");
            }

            // Cập nhật trạng thái của hóa đơn
            existingBill.Status = status;

            // Cập nhật entity trong repository
            context.BillRepository.Update(existingBill);

            // Lưu thay đổi vào cơ sở dữ liệu
            await context.SaveChangesAsync();
        }


        public async Task<int> InsertAsync(BillDTO model)
        {
            try
            {
                // Tạo đối tượng Bill từ model
                var entity = mapper.Map<BillDTO, Bill>(model);

                // Thêm hóa đơn vào repository
                await context.BillRepository.InsertAsync(entity);

                // Lưu thay đổi vào cơ sở dữ liệu
                await context.SaveChangesAsync();
                return entity.Idbill;
                
            }
            catch (DbUpdateException dbEx)
            {
                // Xử lý lỗi liên quan đến cơ sở dữ liệu
                Console.WriteLine($"Database update error: {dbEx.InnerException?.Message ?? dbEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
                // Xử lý lỗi chung
                Console.WriteLine($"An error occurred: {ex.InnerException?.Message ?? ex.Message}");
                throw;
            }
        }

        public async Task<StatisticalResult> GetStatisticalBillAsync(DateTime DateStart, DateTime? DateEnd)
        {
            return await context.BillRepository.GetStatisticalBillAsync(DateStart,DateEnd);


        }

        public async Task<List<ProductSalesDTO>> GetTotalUniqueProductsSoldAsync(DateTime DateStart, DateTime? DateEnd)
        {
            return await context.BillRepository.GetTotalUniqueProductsSoldAsync(DateStart, DateEnd);
        }


    }
}
