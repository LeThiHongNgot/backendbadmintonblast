using AutoMapper;
using BadmintonBlast.DataAccess.Adstraction;
using BadmintonBlast.Models.Dtos;
using BadmintonBlast.Models.Entities;
using BadmintonBlast.Services.Adstraction;

namespace BadmintonBlast.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<OrderDTO>> GetAsync()
        {
            // Lấy tất cả các order dưới dạng DTO
            return await unitOfWork
                .OrderRepository
                .GetAsync<OrderDTO>();
        }

        public async Task<IEnumerable<OrderDTO>> GetByIdAsync(int id)
        {
            // Lấy một đơn hàng (Order) từ repository dựa trên ID
            var order = await unitOfWork.OrderRepository.GetAsync<Order>(o => o.Idbill == id);

            // Kiểm tra xem đơn hàng có tồn tại không
            if (order == null)
            {
                return null; // hoặc xử lý theo cách khác khi không tìm thấy
            }

            // Map từ Order entity sang OrderDTO
            return mapper.Map<IEnumerable<OrderDTO>>(order);
        }

        public async Task InsertAsync(OrderDTO order)
        {
            // Map từ OrderDTO sang Order entity
            var entity = mapper.Map<OrderDTO, Order>(order);

            // Thêm đơn hàng mới vào repository
            await unitOfWork.OrderRepository.InsertAsync(entity);

            // Lưu thay đổi vào cơ sở dữ liệu
            await unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, OrderDTO orderDTO)
        {
            if (orderDTO == null)
            {
                throw new ArgumentNullException(nameof(orderDTO), "Order data is required.");
            }

            // Lấy đơn hàng hiện tại từ cơ sở dữ liệu dựa trên ID
            var existingEntity = await unitOfWork.OrderRepository.GetSingleAsync(id);
            if (existingEntity == null)
            {
                throw new KeyNotFoundException($"Order with ID {id} not found.");
            }

            // Map các giá trị mới từ OrderDTO sang entity hiện tại
            mapper.Map(orderDTO, existingEntity);

            // Cập nhật entity trong repository
            unitOfWork.OrderRepository.Update(existingEntity);

            // Lưu thay đổi vào cơ sở dữ liệu
            await unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            // Xóa đơn hàng dựa trên ID
            await unitOfWork.OrderRepository.DeleteAsync(id);

            // Lưu thay đổi vào cơ sở dữ liệu
            await unitOfWork.SaveChangesAsync();
        }


    }
}
